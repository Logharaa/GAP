using System.ComponentModel;

namespace GAP.CustomControls
{
    public class EqualizerLegend : Control
    {
        private int _adjustableDbLevel = 12;
        private readonly Font _font = new("Inter Medium", 11F, FontStyle.Bold, GraphicsUnit.Point);

        private const string _noDbChange = "0 dB";
        private string _dbAddition;
        private string _dbReduction;

        private readonly Color _textColor = Color.FromArgb(208, 208, 208);

        [DefaultValue(12)]
        public int AdjustableDbLevel
        {
            get => _adjustableDbLevel;
            set
            {
                _adjustableDbLevel = value;
                _dbAddition = "+" + _adjustableDbLevel + " dB";
                _dbReduction = "-" + _adjustableDbLevel + " dB";
                Invalidate();
            }
        }

        public EqualizerLegend()
        {
            SetStyle(ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.ResizeRedraw,
                true);

            _dbAddition = "+" + _adjustableDbLevel + " dB";
            _dbReduction = "-" + _adjustableDbLevel + " dB";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            SizeF dbAdditionStringSize = g.MeasureString(_dbAddition, _font);
            SizeF noDbChangeStringSize = g.MeasureString(_noDbChange, _font);
            SizeF dbReductionStringSize = g.MeasureString(_dbReduction, _font);

            TextRenderer.DrawText(
                g,
                _dbAddition,
                _font,
                new Point(
                    Width - (int)dbAdditionStringSize.Width,
                    0),
                _textColor);

            TextRenderer.DrawText(
                g,
                _noDbChange,
                _font,
                new Point(
                    Width - (int)noDbChangeStringSize.Width,
                    (int)(Height * 0.5 - noDbChangeStringSize.Height * 0.5)),
                _textColor);

            TextRenderer.DrawText(
                g,
                _dbReduction,
                _font,
                new Point(
                    Width - (int)dbReductionStringSize.Width,
                    Height - (int)dbReductionStringSize.Height),
                _textColor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _font.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
