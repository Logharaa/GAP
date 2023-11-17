using Plasmoid.Extensions;
using System.ComponentModel;

namespace GAP.CustomControls
{
    public class RoundedPanel : Panel
    {
        private Color _panelColor = Color.FromArgb(48, 48, 48);
        private int _roundingRadius = 12;

        private SolidBrush _panelBrush;

        [DefaultValue(typeof(Color), "48, 48, 48")]
        public Color PanelColor
        {
            get => _panelColor;
            set
            {
                _panelColor = value;
                _panelBrush = new(value);
                Invalidate();
            }
        }

        [DefaultValue(12)]
        public int RoundingRadius
        {
            get => _roundingRadius;
            set
            {
                _roundingRadius = value;
                Invalidate();
            }
        }

        public RoundedPanel()
        {
            SetStyle(ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor,
                true);

            BackColor = Color.Transparent;
            _panelBrush = new(_panelColor);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.FillRoundedRectangle(
                _panelBrush,
                0,
                0,
                Width,
                Height,
                RoundingRadius);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _panelBrush.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
