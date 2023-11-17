using NAudio.Dsp;
using NAudio.Wave;

namespace GAP.SampleProviders
{
    public class FourierTransformProvider : ISampleProvider
    {
        private readonly ISampleProvider _source;

        private readonly Complex[] _fftBuffer;
        private readonly FftEventArgs _fftArgs;
        private readonly int _m;
        private int _fftIndex = 0;

        public event EventHandler<FftEventArgs>? FftCalculated;

        public FourierTransformProvider(ISampleProvider source, int fftLength = 1024)
        {
            if (!IsPowerOfTwo(fftLength))
                throw new ArgumentException("FFT length must be a power of two.");

            _source = source;

            _fftBuffer = new Complex[fftLength];
            _fftArgs = new FftEventArgs(_fftBuffer);
            _m = (int)Math.Log(fftLength, 2.0);
        }

        private static bool IsPowerOfTwo(int x)
        {
            return (x & x - 1) == 0;
        }

        public WaveFormat WaveFormat => _source.WaveFormat;

        public int Read(float[] buffer, int offset, int count)
        {
            int nbSamples = _source.Read(buffer, offset, count);

            // Only compute FFT if there is an event listener.
            if (FftCalculated != null)
            {
                for (int n = 0; n < nbSamples; n += _source.WaveFormat.Channels)
                {
                    _fftBuffer[_fftIndex].X = (float)(buffer[n + offset] * FastFourierTransform.HannWindow(_fftIndex, _fftBuffer.Length));
                    _fftBuffer[_fftIndex].Y = 0;
                    _fftIndex++;

                    if (_fftIndex >= _fftBuffer.Length)
                    {
                        _fftIndex = 0;
                        FastFourierTransform.FFT(true, _m, _fftBuffer);
                        FftCalculated.Invoke(this, _fftArgs);
                    }
                }
            }

            return nbSamples;
        }
    }

    public class FftEventArgs : EventArgs
    {
        public Complex[] Results { get; private set; }

        public FftEventArgs(Complex[] results)
        {
            Results = results;
        }
    }
}
