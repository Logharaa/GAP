using Plasmoid.Extensions;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace GAP.CustomControls
{
    public class HorizontalSlider : Control
    {
        private int _minimum = 0;
        private int _maximum = 16;
        private int _value = 0;
        private bool _isDragging = false;
        private int _sliderBarHeight = 7;
        private int _knobRadius = 12;

        private readonly SolidBrush _sliderBarBrush = new(Color.FromArgb(78, 78, 78));
        private readonly SolidBrush _sliderOffsetBarBrush = new(Color.FromArgb(255, 148, 112));
        private readonly SolidBrush _sliderKnobBrush = new(Color.FromArgb(255, 255, 255));

        public delegate void OnValueChanged(int oldValue, int newValue);
        public event OnValueChanged? ValueChanged;

        public event EventHandler<int>? FinishedDragging;

        [DefaultValue(0)]
        public int Minimum
        {
            get => _minimum;
            set
            {
                if (value > Maximum)
                    Maximum = value;

                if (Value < value)
                    Value = value;

                _minimum = value;
            }
        }

        [DefaultValue(16)]
        public int Maximum
        {
            get => _maximum;
            set
            {
                if (value < Minimum)
                    Minimum = value;

                if (Value > value)
                    Value = value;

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

        [DefaultValue(7)]
        public int SliderBarHeight
        {
            get => _sliderBarHeight;
            set
            {
                _sliderBarHeight = value;
                Invalidate();
            }
        }

        [DefaultValue(12)]
        public int KnobRadius
        {
            get => _knobRadius;
            set
            {
                _knobRadius = value;
                Invalidate();
            }
        }

        public HorizontalSlider()
        {
            SetStyle(ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.OptimizedDoubleBuffer,
                true);

            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Top left corner coordinates of the slider bar.
            int sliderBarX = KnobRadius;
            int sliderBarY = (int)(Height*0.5 - SliderBarHeight*0.5);

            int sliderBarWidth = GetSliderBarWidth();

            g.FillRoundedRectangle(
                _sliderBarBrush,
                sliderBarX,
                sliderBarY,
                sliderBarWidth,
                SliderBarHeight,
                8);

            // Convert slider range to pixel range (linear conversion).
            int sliderKnobX;
            if (Maximum == Minimum)
                sliderKnobX = 0;
            else
                sliderKnobX = ((Value - Minimum) * sliderBarWidth) / (Maximum - Minimum);

            if (sliderKnobX > 0)
            {
                g.FillRoundedRectangle(
                    _sliderOffsetBarBrush,
                    sliderBarX,
                    sliderBarY,
                    sliderKnobX,
                    SliderBarHeight,
                    8);
            }

            g.FillEllipse(
                _sliderKnobBrush,
                sliderKnobX,
                sliderBarY - KnobRadius + SliderBarHeight/2,
                KnobRadius * 2 - 1,
                KnobRadius * 2 - 1);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                UpdateCurrentValue(e.Location.X);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_isDragging)
                UpdateCurrentValue(e.Location.X);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_isDragging)
            {
                _isDragging = false;
                FinishedDragging?.Invoke(this, Value);
            }
        }

        private int GetSliderBarWidth()
        {
            // Control width can change at runtime when the window is resized.
            return Width - KnobRadius * 2;
        }

        private void UpdateCurrentValue(int mouseX)
        {
            // Convert pixel range to slider range (linear conversion).
            Value = ((mouseX - KnobRadius) * (Maximum - Minimum) / GetSliderBarWidth()) + Minimum;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sliderBarBrush.Dispose();
                _sliderOffsetBarBrush.Dispose();
                _sliderKnobBrush.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
