using Plasmoid.Extensions;
using System.ComponentModel;
using System.Diagnostics;
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
                int newValue;

                // Make sure newValue is within the range [Minimum, Maximum].
                if (value < Minimum)
                    newValue = Minimum;
                else if (value > Maximum)
                    newValue = Maximum;
                else
                    newValue = value;

                ValueChanged?.Invoke(_value, newValue);
                _value = newValue;

                Invalidate();
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

            using (SolidBrush sliderBarBrush = new(Color.FromArgb(78, 78, 78)))
            {
                g.FillRoundedRectangle(
                    sliderBarBrush,
                    sliderBarX,
                    sliderBarY,
                    sliderBarWidth,
                    SliderBarHeight,
                    8);
            }

            // Convert slider range to pixel range (linear conversion).
            int sliderKnobX;
            if (Maximum == Minimum)
                sliderKnobX = 0;
            else
                sliderKnobX = ((Value - Minimum) * sliderBarWidth) / (Maximum - Minimum);

            if (sliderKnobX > 0)
            {
                using (SolidBrush sliderOffsetBarBrush = new(Color.FromArgb(255, 148, 112)))
                {
                    g.FillRoundedRectangle(
                        sliderOffsetBarBrush,
                        sliderBarX,
                        sliderBarY,
                        sliderKnobX,
                        SliderBarHeight,
                        8);
                }
                
            }

            using (SolidBrush sliderKnobBrush = new(Color.FromArgb(255, 255, 255)))
            {
                g.FillEllipse(
                    sliderKnobBrush,
                    sliderKnobX,
                    sliderBarY - KnobRadius + (int)(SliderBarHeight * 0.5),
                    KnobRadius * 2 - 1,
                    KnobRadius * 2 - 1);
            }
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
            int newValue = ((mouseX - KnobRadius) * (Maximum - Minimum) / GetSliderBarWidth()) + Minimum;

            if (newValue >= Minimum
                && newValue <= Maximum
                && newValue != Value)
            {
                Value = newValue;
            }
        }
    }
}
