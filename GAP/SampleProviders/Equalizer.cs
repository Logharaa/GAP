using NAudio.Dsp;
using NAudio.Wave;

namespace GAP.SampleProviders
{
    public class Equalizer : ISampleProvider
    {
        private readonly ISampleProvider _source;
        private readonly BiQuadFilter[,] _filters;

        public readonly EqualizerBand[] bands;

        public Equalizer(ISampleProvider source, EqualizerBand[] eqBands)
        {
            _source = source;
            bands = eqBands;
            _filters = new BiQuadFilter[source.WaveFormat.Channels, bands.Length];
            InitFilters();
        }

        private void InitFilters()
        {
            for (int bandIndex = 0; bandIndex < bands.Length; bandIndex++)
            {
                EqualizerBand currentBand = bands[bandIndex];

                for (int n = 0; n < _source.WaveFormat.Channels; n++)
                {
                    _filters[n, bandIndex] = BiQuadFilter.PeakingEQ(_source.WaveFormat.SampleRate, currentBand.Frequency, currentBand.Bandwidth, currentBand.Gain);
                }
            }
        }

        public void UpdateGain(int bandIndex, float newGain)
        {
            EqualizerBand band = bands[bandIndex];

            band.Gain = newGain;

            for (int n = 0; n < _source.WaveFormat.Channels; n++)
            {
                _filters[n, bandIndex].SetPeakingEq(_source.WaveFormat.SampleRate, band.Frequency, band.Bandwidth, band.Gain);
            }
        }

        public WaveFormat? WaveFormat => _source.WaveFormat;

        public int Read(float[] buffer, int offset, int count)
        {
            int nbSamples = _source.Read(buffer, offset, count);

            for (int n = 0; n < nbSamples; n++)
            {
                int channel = n % _source.WaveFormat.Channels;

                for (int bandIndex = 0; bandIndex < bands.Length; bandIndex++)
                {
                    buffer[offset + n] = _filters[channel, bandIndex].Transform(buffer[offset + n]);
                }
            }

            return nbSamples;
        }
    }

    public class EqualizerBand
    {
        public float Frequency { get; set; }
        public float Bandwidth { get; set; }
        public float Gain { get; set; }
        
        public EqualizerBand(float freq, float bandwidth, float gain)
        {
            Frequency = freq;
            Bandwidth = bandwidth;
            Gain = gain;
        }
    }
}
