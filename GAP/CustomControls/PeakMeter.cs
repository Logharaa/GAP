using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace GAP.CustomControls
{
    public class PeakMeter : Control
    {
        private float _amplitude = 0.0001f; // -80 dB
        private float _smoothNormalizedDb = 0.0f;
        private System.Windows.Forms.Timer _timer = new();

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
        public float MaxDb { get; set; } = 6.0f;

        [DefaultValue(-48.0f)]
        public float MinDb { get; set; } = -48.0f;

        public float DecibelsRange
        {
            get => MaxDb - MinDb;
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

            _timer.Interval = 1000 / 48; // 48 ticks per second.
            _timer.Tick += PeakMeter_Tick;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            int zeroDbLocationY = Height - (int)(-MinDb * Height / DecibelsRange); // Linear conversion.

            // Background of the peak meter.
            using (SolidBrush backgroundBrush = new(Color.FromArgb(48, 48, 48)))
            {
                g.FillRectangle(backgroundBrush, 0, 0, Width, Height);
            }

            using (Pen linePen = new(Color.FromArgb(68, 68, 68), 1))
            {
                g.DrawLine(linePen,
                    5,
                    zeroDbLocationY,
                    Width - 5,
                    zeroDbLocationY);
            }

            float decibels = GetDecibelsFromAmplitude(Amplitude);

            // Normalize decibels to [0, 1] range.
            float normalizedDb = (decibels - MinDb) / DecibelsRange;

            // Smooth the signal.
            if (normalizedDb > _smoothNormalizedDb)
            {
                _smoothNormalizedDb = normalizedDb;
            }
            else
            {
                _smoothNormalizedDb -= (_smoothNormalizedDb - normalizedDb) * SmoothFactor;

                if (_smoothNormalizedDb < 0.001f)
                    _smoothNormalizedDb = 0.0f;
            }

            int peakRectHeight = (int)(Height * _smoothNormalizedDb);

            using LinearGradientBrush rectBrush = new(
                new Point(0, Height),
                new Point(0, 0),
                Color.FromArgb(255, 255, 255),
                Color.FromArgb(255, 148, 112));

            if (decibels > 0)
            {
                g.FillRectangle(
                    rectBrush,
                    0,
                    Height - zeroDbLocationY,
                    Width,
                    zeroDbLocationY);

                using (SolidBrush clippingBrush = new(Color.FromArgb(163, 24, 24)))
                {
                    g.FillRectangle(
                        clippingBrush,
                        0,
                        Height - peakRectHeight,
                        Width,
                        peakRectHeight - zeroDbLocationY);
                }
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
            if (decibels > MaxDb)
                return MaxDb;

            return (float)decibels;
        }

        public void StopSmoothly()
        {
            Amplitude = 0.0f;
            _timer.Start();
        }

        private void PeakMeter_Tick(object? sender, EventArgs e)
        {
            if (_smoothNormalizedDb == 0.0f)
                _timer.Stop();
            else
                Invalidate();
        }
    }
}
