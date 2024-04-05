using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MakeMyTripClone.UserControls
{
    public class CustomButton : Button
    {
        private Color borderColor = Color.LightGray;
        private int cornerRadius = 10;

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }

        public int CornerRadius
        {
            get { return cornerRadius; }
            set { cornerRadius = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Pen borderPen = new Pen(borderColor, 1))
            {
                // Draw rounded rectangle border
                e.Graphics.DrawRoundedRectangle(borderPen, 0, 0, Width - 1, Height - 1, cornerRadius);
            }
        }
    }
}
