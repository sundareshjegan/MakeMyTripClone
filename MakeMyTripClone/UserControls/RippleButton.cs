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
    class RippleButton : Button
    {
        private int rippleSize;
        private List<Ripple> clicks = new List<Ripple>();
        private Timer t = new Timer();
        private int count = 0;
        private const int grab = 16;

        public RippleButton()
        {
            this.MouseDown += RippleButton_MouseDown;
            this.Paint += RippleButton_Paint;
            //this.ResizeRedraw = true;
        }

        public void RippleButton_MouseDown(object sender, MouseEventArgs e)
        {
            clicks.Add(new MakeMyTripClone.Ripple { X = e.X, Y = e.Y, Size = 0 });

            if (count == 0)
            {
                count++;
                t = new Timer();
                t.Interval = 15;
                t.Tick += RippleTimer_Tick;
                t.Start();
            }

        }

        private void RippleTimer_Tick(object sender, EventArgs e)
        {
            for (int Iter = 0; Iter < clicks.Count; Iter++)
            {
                clicks[Iter].Size += 5;
            }

            this.Invalidate();

            for (int Iter = 0; Iter < clicks.Count; Iter++)
            {

                if (clicks[Iter].Size > (Width + Height))
                {
                    if (clicks.Count == 1)
                    {
                        t.Stop();
                        count = 0;
                    }
                    rippleSize = clicks[Iter].Size = 0;
                    clicks.RemoveAt(Iter);
                }
            }

        }

        private void RippleButton_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            foreach (var Iter in clicks)
            {
                if (Iter.Size > 0)
                {
                    DoubleBuffered = true;
                    Color nonIntersectingColor = Color.FromArgb(40, Color.AliceBlue);
                    var b = new SolidBrush(nonIntersectingColor);
                    g.FillEllipse(b, Iter.X - (Iter.Size), Iter.Y - (Iter.Size), 2 * Iter.Size, 2 * Iter.Size);



                    b.Dispose();
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84)
            {
                var pos = this.PointToClient(new Point(m.LParam.ToInt32()));
                if (pos.X >= this.ClientSize.Width - grab && pos.Y >= this.ClientSize.Height - grab)
                    m.Result = new IntPtr(17);
            }
        }
    }

    class Ripple
    {
        public int X, Y, Size;
    }
}
