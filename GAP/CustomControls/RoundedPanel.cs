using Plasmoid.Extensions;
using System.ComponentModel;

namespace GAP.CustomControls
{
    public class RoundedPanel : Panel
    {
        private Color _panelColor = Color.FromArgb(48, 48, 48);
        private int _roundingRadius = 12;

        [DefaultValue(typeof(Color), "48, 48, 48")]
        public Color PanelColor
        {
            get => _panelColor;
            set
            {
                _panelColor = value;
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
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (SolidBrush panelBrush = new(PanelColor))
            {
                e.Graphics.FillRoundedRectangle(
                    panelBrush,
                    0,
                    0,
                    Width,
                    Height,
                    RoundingRadius);
            }
        }
    }
}
