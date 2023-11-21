using Plasmoid.Extensions;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace GAP.CustomControls
{
    public class EqualizerBand : Control
    {
        private int _minimum = -120;
        private int _maximum = 120;
        private int _value = 0;
        private int _valueRange = 240;

        private bool _isDragging = false;

        private readonly int _sliderBarWidth = 7;
        private readonly Size _thumbSize = new(40, 20);
        private readonly int _graduationsMargin = 6;

        private readonly SolidBrush _sliderBarBrush = new(Color.FromArgb(78, 78, 78));
        private readonly SolidBrush _sliderOffsetBarBrush = new(Color.FromArgb(255, 148, 112));
        private readonly SolidBrush _thumbBrush = new(Color.FromArgb(245, 245, 245));
        private readonly Pen _graduationsPen = new(Color.FromArgb(64, 64, 64), 1);
        private readonly Pen _thumbLinePen = new(Color.FromArgb(255, 148, 112), 3);

        private ContextMenuStrip _contextMenu = new();

        public event EventHandler<int>? ValueChanged;

        [DefaultValue(-120)]
        public int Minimum
        {
            get => _minimum;
            set
            {
                if (value > Maximum)
                    Maximum = value;

                if (Value < value)
                    Value = value;

                _valueRange = Maximum - value;
                _minimum = value;
            }
        }

        [DefaultValue(120)]
        public int Maximum
        {
            get => _maximum;
            set
            {
                if (value < Minimum)
                    Minimum = value;

                if (Value > value)
                    Value = value;

                _valueRange = value - Minimum;
                _maximum = value;
            }
        }

        [DefaultValue(0)]
        public int Value
        {
            get => _value;
            set
            {
                // Make sure value is within the range [Minimum, Maximum].
                if (value < Minimum)
                    value = Minimum;
                else if (value > Maximum)
                    value = Maximum;

                if (_value != value)
                {
                    ValueChanged?.Invoke(_value, value);
                    _value = value;
                    Invalidate();
                }
            }
        }

        public EqualizerBand()
        {
            SetStyle(ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.OptimizedDoubleBuffer,
                true);

            BackColor = Color.Transparent;

            _contextMenu.ForeColor = Color.FromArgb(255, 255, 255);
            _contextMenu.Renderer = new GapMenuStripRenderer();
            _contextMenu.Items.Add("Reset", null, new EventHandler(ContextMenuReset_Click));
            ContextMenuStrip = _contextMenu;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Top left corner coordinates of the slider bar.
            int sliderBarX = (int)(Width * 0.5 - _sliderBarWidth * 0.5);
            int sliderBarY = _thumbSize.Height / 2;

            int sliderBarHeight = Height - _thumbSize.Height - 1;

            // Display the slider bar.
            g.FillRoundedRectangle(
                _sliderBarBrush,
                sliderBarX,
                sliderBarY,
                _sliderBarWidth,
                sliderBarHeight,
                8);

            // Get thumb Y coordinate (top left corner) by converting Value
            // from slider value range to pixel range (linear conversion).
            int thumbY;
            int maxThumbY = Height - _thumbSize.Height - 1;

            if (Maximum == Minimum)
                thumbY = (int)(Height * 0.5 - _thumbSize.Height * 0.5); // Center vertically.
            else
                thumbY = (-Value - Minimum) * maxThumbY / _valueRange;

            int thumbX = (int)(Width * 0.5 - _thumbSize.Width * 0.5);

            // Display the colored offset bar.
            int offsetBarY = thumbY + _thumbSize.Height / 2;

            if (Value > 0)
            {
                g.FillRectangle(
                    _sliderOffsetBarBrush,
                    sliderBarX,
                    offsetBarY,
                    _sliderBarWidth,
                    Height - offsetBarY - Height / 2);
            }
            else if (Value < 0)
            {
                g.FillRectangle(
                    _sliderOffsetBarBrush,
                    sliderBarX,
                    Height / 2,
                    _sliderBarWidth,
                    offsetBarY - Height / 2);
            }

            // Display the graduation lines.
            int quarterSliderBarHeight = sliderBarHeight / 4;

            for (int i = 0; i < 5; i++)
            {
                int currentGraduationsY = sliderBarY + i * quarterSliderBarHeight;

                // Left graduation line.
                g.DrawLine(
                    _graduationsPen,
                    thumbX,
                    currentGraduationsY,
                    sliderBarX - _graduationsMargin,
                    currentGraduationsY);

                // Right graduation line.
                g.DrawLine(
                    _graduationsPen,
                    sliderBarX + _sliderBarWidth + _graduationsMargin,
                    currentGraduationsY,
                    thumbX + _thumbSize.Width,
                    currentGraduationsY);
            }

            // Display the thumb.
            g.FillRoundedRectangle(
                _thumbBrush,
                thumbX,
                thumbY,
                _thumbSize.Width,
                _thumbSize.Height,
                2);

            // Display the thumb line.
            int thumbLineY = thumbY + _thumbSize.Height / 2;

            g.DrawLine(
                _thumbLinePen,
                Width / 2 - 10,
                thumbLineY,
                Width / 2 + 10,
                thumbLineY);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                UpdateValue(e.Location.Y);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_isDragging)
                UpdateValue(e.Location.Y);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_isDragging)
                _isDragging = false;
        }

        private void UpdateValue(int mouseY)
        {
            // Get slider value by converting mouse Y coordinate
            // from pixel range to slider value range (linear conversion).
            int maxThumbY = Height - _thumbSize.Height - 1;

            Value = (((mouseY - _thumbSize.Height / 2) * _valueRange / maxThumbY) + Minimum) * -1;
        }

        private void ContextMenuReset_Click(object? sender, EventArgs e)
        {
            Value = 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sliderBarBrush.Dispose();
                _sliderOffsetBarBrush.Dispose();
                _thumbBrush.Dispose();
                _graduationsPen.Dispose();
                _thumbLinePen.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
