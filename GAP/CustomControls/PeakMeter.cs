using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace GAP.CustomControls
{
    public class PeakMeter : Control
    {
        private float _amplitude = 0.0001f; // -80 dB
        
        private float _maxDb = 6.0f;
        private float _minDb = -48.0f;
        private float _decibelsRange = 54.0f;
        private float _smoothDbNormalized = 0.0f;

        private readonly SolidBrush _backgroundBrush = new(Color.FromArgb(34, 34, 34));
        private readonly Pen _zeroDbLinePen = new(Color.FromArgb(54, 54, 54), 1);
        private readonly SolidBrush _clippingBrush = new(Color.FromArgb(255, 0, 0));
        private readonly System.Windows.Forms.Timer _smoothStopTimer = new();

        public float Amplitude
        {
            get => _amplitude;
            set
            {
                _amplitude = value;
                Invalidate();
            }
        }

        [DefaultValue(6.0f)]
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

        [DefaultValue(-48.0f)]
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

        public PeakMeter()
        {
            SetStyle(ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.OptimizedDoubleBuffer,
                true);

            _smoothStopTimer.Interval = 1000 / 48; // 48 ticks per second.
            _smoothStopTimer.Tick += PeakMeter_Tick;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Background of the peak meter.
            g.FillRectangle(_backgroundBrush, 0, 0, Width, Height);

            int zeroDbLocationY = Height - (int)(-MinDb * Height / _decibelsRange); // Linear conversion.
            g.DrawLine(_zeroDbLinePen, 5, zeroDbLocationY, Width - 5, zeroDbLocationY);

            float decibels = GetDecibelsFromAmplitude(Amplitude);

            // Normalize decibels to [0, 1] range.
            float dbNormalized = (decibels - MinDb) / _decibelsRange;

            // Interpolate with previous value to smooth the movement.
            if (dbNormalized > _smoothDbNormalized)
            {
                _smoothDbNormalized = dbNormalized;
            }
            else
            {
                _smoothDbNormalized -= (_smoothDbNormalized - dbNormalized) * SmoothFactor;

                if (_smoothDbNormalized < 0.001f)
                    _smoothDbNormalized = 0.0f;
            }

            int peakRectHeight = (int)(Height * _smoothDbNormalized);

            using LinearGradientBrush rectBrush = new(
                new Point(0, Height),
                new Point(0, 0),
                Color.FromArgb(255, 255, 255),
                Color.FromArgb(255, 148, 112));

            // Display a red rectangle when the sound is clipping.
            if (decibels > 0)
            {
                int zeroDbRectHeight = Height - zeroDbLocationY;

                g.FillRectangle(
                    rectBrush,
                    0,
                    Height - zeroDbRectHeight,
                    Width,
                    zeroDbRectHeight);

                g.FillRectangle(
                    _clippingBrush,
                    0,
                    Height - peakRectHeight,
                    Width,
                    peakRectHeight - zeroDbRectHeight);
            }
            else
            {
                g.FillRectangle(
                    rectBrush,
                    0,
                    Height - peakRectHeight,
                    Width,
                    peakRectHeight);
            }
        }

        private float GetDecibelsFromAmplitude(float amplitude)
        {
            if (amplitude == 0)
                return MinDb;

            double decibels = 20 * Math.Log10(amplitude);

            if (decibels < MinDb)
                return MinDb;
            else if (decibels > MaxDb)
                return MaxDb;

            return (float)decibels;
        }

        public void StopSmoothly()
        {
            Amplitude = 0.0f;
            _smoothStopTimer.Start();
        }

        public void EndStoppingAnimation()
        {
            _smoothStopTimer.Stop();
        }

        private void PeakMeter_Tick(object? sender, EventArgs e)
        {
            if (_smoothDbNormalized == 0.0f)
                _smoothStopTimer.Stop();
            else
                Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _backgroundBrush.Dispose();
                _zeroDbLinePen.Dispose();
                _clippingBrush.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
