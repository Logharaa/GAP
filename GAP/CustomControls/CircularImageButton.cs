using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace GAP.CustomControls
{
    public class CircularImageButton : Control
    {
        private Image? _buttonImage;
        private Size _buttonImageSize = new(32, 32);
        private Color _buttonDefaultColor = Color.FromArgb(208, 208, 208);
        private Color _buttonHoveredColor = Color.FromArgb(255, 255, 255);
        private Color _buttonPressedColor = Color.FromArgb(128, 128, 128);
        private enum ButtonState { Default, Hovered, Pressed }
        private ButtonState _buttonState = ButtonState.Default;

        public Image? ButtonImage
        {
            get => _buttonImage;
            set
            {
                _buttonImage?.Dispose();
                _buttonImage = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Size), "32, 32")]
        public Size ButtonImageSize
        {
            get => _buttonImageSize;
            set
            {
                _buttonImageSize = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "208, 208, 208")]
        public Color ButtonDefaultColor
        {
            get => _buttonDefaultColor;
            set
            {
                _buttonDefaultColor = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "255, 255, 255")]
        public Color ButtonHoveredColor
        {
            get => _buttonHoveredColor;
            set
            {
                _buttonHoveredColor = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "128, 128, 128")]
        public Color ButtonPressedColor
        {
            get => _buttonPressedColor;
            set
            {
                _buttonPressedColor = value;
                Invalidate();
            }
        }

        public CircularImageButton()
        {
            SetStyle(ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
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

            SolidBrush buttonBrush;

            if (_buttonState == ButtonState.Default)
                buttonBrush = new(ButtonDefaultColor);
            else if (_buttonState == ButtonState.Hovered)
                buttonBrush = new(ButtonHoveredColor);
            else if (_buttonState == ButtonState.Pressed)
                buttonBrush = new(ButtonPressedColor);
            else
                buttonBrush = new(ButtonDefaultColor);

            g.FillEllipse(
                buttonBrush,
                0,
                0,
                Width - 1,
                Height - 1);

            buttonBrush.Dispose();

            if (ButtonImage != null)
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Top left corner coordinates of the image.
                int imageX = (int)(Width * 0.5) - (int)(ButtonImageSize.Width * 0.5);
                int imageY = (int)(Height * 0.5) - (int)(ButtonImageSize.Height * 0.5);

                g.DrawImage(
                    ButtonImage,
                    imageX,
                    imageY,
                    ButtonImageSize.Width,
                    ButtonImageSize.Height);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            _buttonState = ButtonState.Hovered;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            _buttonState = ButtonState.Default;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            _buttonState = ButtonState.Pressed;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (ClientRectangle.Contains(e.Location))
                _buttonState = ButtonState.Hovered;
            else
                _buttonState = ButtonState.Default;

            Invalidate();
        }
    }
}
