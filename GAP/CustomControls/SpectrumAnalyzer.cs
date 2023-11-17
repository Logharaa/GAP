using NAudio.Dsp;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace GAP.CustomControls
{
    public class SpectrumAnalyzer : Control
    {
        private Complex[]? _fftResults;

        // The number of frequency bins is half the FFT array length.
        // By default, we assume that the FFT array length is 1024.
        private int _nbFrequencyBins = 512;

        private float[] _smoothDbNormalized = new float[512];
        private PointF[] _fftCurvePoints = new PointF[512];

        private float _maxDb = 0.0f;
        private float _minDb = -80.0f;
        private float _decibelsRange = 80.0f;

        private readonly SolidBrush _backgroundBrush = new(Color.FromArgb(34, 34, 34));
        private readonly SolidBrush _fftCurveBrush = new(Color.FromArgb(208, 208, 208));
        private readonly System.Windows.Forms.Timer _smoothStopTimer = new();

        public Complex[]? FftResults
        {
            get => _fftResults;
            set
            {
                _fftResults = value;

                if (value != null && value.Length / 2 != _nbFrequencyBins)
                {
                    _nbFrequencyBins = value.Length / 2;
                    _smoothDbNormalized = new float[_nbFrequencyBins];
                    _fftCurvePoints = new PointF[_nbFrequencyBins];
                }

                Invalidate();
            }
        }

        [DefaultValue(0.0f)]
        public float MaxDb
        {
            get => _maxDb;
            set
            {
                _maxDb = value;
                _decibelsRange = value - _minDb;
                Invalidate();
            }
        }

        [DefaultValue(-80.0f)]
        public float MinDb
        {
            get => _minDb;
            set
            {
                _minDb = value;
                _decibelsRange = _maxDb - value;
                Invalidate();
            }
        }

        [DefaultValue(0.18f)]
        public float SmoothFactor { get; set; } = 0.18f;

        public SpectrumAnalyzer()
        {
            SetStyle(ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.OptimizedDoubleBuffer,
                true);

            _smoothStopTimer.Interval = 1000 / 48; // 48 ticks per second.
            _smoothStopTimer.Tick += SpectrumAnalyzer_Tick;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Background of the spectrum analyzer.
            g.FillRectangle(_backgroundBrush, 0, 0, Width, Height);

            if (FftResults != null)
            {
                float binsSpacing = (float)Width / _nbFrequencyBins;

                for (int n = 1; n < _nbFrequencyBins - 1; n++)
                {
                    float binLocationX = Width - n * binsSpacing;
                    float decibels = GetDecibelsFromComplex(FftResults[_nbFrequencyBins + n]);
                    
                    // Normalize decibels to [0, 1] range.
                    float dbNormalized = (decibels - MinDb) / _decibelsRange;

                    // Interpolate with previous value to smooth the movement.
                    _smoothDbNormalized[n] += (dbNormalized - _smoothDbNormalized[n]) * SmoothFactor;

                    if (_smoothDbNormalized[n] < 0.001f)
                        _smoothDbNormalized[n] = 0.0f;

                    // Convert bin X location to logarithmic scale and normalize to [0, 1] range.
                    float logXnormalized = (float)(Math.Log10(binLocationX) / Math.Log10(Width));

                    _fftCurvePoints[n] = new PointF(Width * logXnormalized, Height - _smoothDbNormalized[n] * Height);
                }

                _fftCurvePoints[0] = new PointF(Width, Height);
                _fftCurvePoints[_nbFrequencyBins - 1] = new PointF(0, Height);

                g.FillClosedCurve(_fftCurveBrush, _fftCurvePoints);
            }
        }

        private float GetDecibelsFromComplex(Complex c)
        {
            double magnitude = Math.Sqrt(c.X*c.X + c.Y*c.Y);
            double decibels = 20 * Math.Log10(magnitude);

            if (decibels < MinDb)
                return MinDb;
            else if (decibels > MaxDb)
                return MaxDb;

            return (float)decibels;
        }

        public void StopSmoothly()
        {
            if (_fftResults != null)
            {
                for (int i = 0; i < _fftResults.Length; i++)
                {
                    _fftResults[i].X = 0.0f;
                    _fftResults[i].Y = 0.0f;
                }

                _smoothStopTimer.Start();
            }
        }
        
        public void EndStoppingAnimation()
        {
            _smoothStopTimer.Stop();
        }

        private void SpectrumAnalyzer_Tick(object? sender, EventArgs e)
        {
            for (int i = 0; i < _nbFrequencyBins; i++)
            {
                if (_smoothDbNormalized[i] != 0.0f)
                {
                    Invalidate();
                    return;
                }
            }

            _smoothStopTimer.Stop();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _backgroundBrush.Dispose();
                _fftCurveBrush.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
