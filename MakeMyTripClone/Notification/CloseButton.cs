using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeMyTripClone
{
    class CloseButton : UserControl
    {
        private System.ComponentModel.IContainer components;
        private Brush b = new SolidBrush(Color.FromArgb(52, 104, 192));

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            GraphicsPath path1 = new GraphicsPath();
            GraphicsPath path2 = new GraphicsPath();
            g.SmoothingMode = SmoothingMode.AntiAlias;

            path1.StartFigure();
            path1.AddArc(new Rectangle(Width / 5, Height / 5, Width / 5, Height / 5), 135, 180);
            path1.AddArc(new Rectangle(Width * 3 / 5, Height * 3 / 5, Width / 5, Height / 5), 315, 180);
            path1.CloseAllFigures();

            path2.StartFigure();
            path2.AddArc(new Rectangle(Width * 3 / 5, Height / 5, Width / 5, Height / 5), 225, 180);
            path2.AddArc(new Rectangle(Width / 5, Height * 3 / 5, Width / 5, Height / 5), 45, 180);
            path2.CloseAllFigures();

            g.FillPath(b, path1);
            g.FillPath(b, path2);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            if (b != null)
                b.Dispose();
            b = new SolidBrush(Color.FromArgb(134, 167, 252));
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (b != null)
                b.Dispose();
            b = new SolidBrush(Color.FromArgb(52, 104, 192));
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (b != null)
                b.Dispose();
            b = new SolidBrush(Color.FromArgb(134, 167, 252));
            this.Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            Width = Height;
            this.Invalidate();
        }

        protected override void OnLoad(EventArgs e)
        {
            DoubleBuffered = true;
            BackColor = Color.Transparent;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.Name = "CloseButton";
            this.ResumeLayout(false);

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
