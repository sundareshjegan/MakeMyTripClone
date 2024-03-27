using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeMyTripClone
{
    public partial class NotificationTemplate : Form
    {
        public bool isContentOutOfHeight = false;
        static public bool isFullContentDisplayed = false;

        public string NotificationHeader
        {
            get
            {
                return header;
            }

            set
            {
                header = value;
                headerPanel.Invalidate();
            }
        }
        public string ContentMessage
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
                SplitContentToWords(message);
                contentPanel.Invalidate();
            }
        }

        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }

            set
            {
                borderRadius = value;
            }
        }
        public DateTime NotificationTime
        {
            set
            {
                time = value.ToString();
            }
        }
        public int TickCount = 0;

        public event EventHandler NotificationClosed;


        public NotificationTemplate()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        public Size SetNotificationSize()
        {
            contentFont = new Font(new FontFamily("Microsoft PhagsPa"), 12);
            contentMessage = GenerateContent(contentPanel.CreateGraphics());
            var contentSize = contentPanel.CreateGraphics().MeasureString(contentMessage, contentFont);
            Size notificationSize;
            if (Width < contentSize.Width)
            {
                notificationSize = new Size(Width, (int)(contentSize.Height + 20 + timePanel.Height + headerPanel.Height + contentSize.Width*43/Width));
            }
            else
            {
                notificationSize = new Size(Width, (int)contentSize.Height + 20 + timePanel.Height + headerPanel.Height);
            }

            if (notificationSize.Height > Screen.PrimaryScreen.Bounds.Height)
            {
                isContentOutOfHeight = true;
                seeMorePanel.Visible = true;
                seeMorePanel.Invalidate();
            }

            return notificationSize;
        }

        private void NotificationTemplate_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = Color.FromArgb(1,1,1);
            this.BackColor = Color.FromArgb(1,1,1);
        }

        private void NotificationTemplate_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            Brush b = new SolidBrush(Color.FromArgb(253, 240, 209));
            linePen = new Pen(Color.Brown);
            linePen.DashStyle = DashStyle.Dot;
            Rectangle rectangle = new Rectangle(1, 1, this.Width - 2, this.Height - 2);
            FillRoundRectangle(e.Graphics, rectangle, b, BorderRadius);
            e.Graphics.DrawLine(linePen, new Point(Width/5, contentPanel.Location.Y), new Point(Width * 4 / 5, contentPanel.Location.Y));
            e.Graphics.DrawLine(linePen, new Point(Width/5, timePanel.Location.Y), new Point(Width * 4 / 5, timePanel.Location.Y));
            b.Dispose();
        }

        private static GraphicsPath GetRoundRectangle(Rectangle rectangle, int r)
        {
            int l = 2 * r;
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(new Point(rectangle.X + r, rectangle.Y), new Point(rectangle.Right - r, rectangle.Y));
            gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Y, l, l), 270F, 90F);

            gp.AddLine(new Point(rectangle.Right, rectangle.Y + r), new Point(rectangle.Right, rectangle.Bottom - r));
            gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Bottom - l, l, l), 0F, 90F);

            gp.AddLine(new Point(rectangle.Right - r, rectangle.Bottom), new Point(rectangle.X + r, rectangle.Bottom));
            gp.AddArc(new Rectangle(rectangle.X, rectangle.Bottom - l, l, l), 90F, 90F);

            gp.AddLine(new Point(rectangle.X, rectangle.Bottom - r), new Point(rectangle.X, rectangle.Y + r));
            gp.AddArc(new Rectangle(rectangle.X, rectangle.Y, l, l), 180F, 90F);

            return gp;
        }

        private void FillRoundRectangle(Graphics g, Rectangle rectangle, Brush b, int r)
        {
            rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            g.FillPath(b, GetRoundRectangle(rectangle, r));
            Pen p = new Pen(Color.Black, 3);
            p.DashStyle = DashStyle.Solid;
            g.DrawPath(p, GetRoundRectangle(rectangle, r));
            p.Dispose();
        }

        private void OnNotificationClosed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NotificationTemplate_FormClosed(object sender, FormClosedEventArgs e)
        {
            NotificationClosed?.Invoke(this, e);
        }

        private void frmFadeClose_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Opacity > 0.01f)
            {
                e.Cancel = true;
                fadeOutTimer.Tick += Timer1_Tick;
                fadeOutTimer.Interval = 50;
                fadeOutTimer.Start();
            }
            else
            {
                fadeOutTimer.Enabled = false;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0.01)
                this.Opacity = this.Opacity - 0.1f;
            else
                this.Close();
        }

        private void OnHeaderPanelPaint(object sender, PaintEventArgs e)
        {
            SFormat = new StringFormat();
            SFormat.Alignment = StringAlignment.Near;
            SFormat.LineAlignment = StringAlignment.Center;

            headerFont = new Font(new FontFamily("Microsoft PhagsPa"), 22, FontStyle.Bold);
            textBrush = new SolidBrush(Color.FromArgb(52, 104, 192));
            stringRectangle = new Rectangle(Padding.Left, Padding.Top, headerPanel.Width, headerPanel.Height);

            e.Graphics.DrawString(header, headerFont, textBrush, stringRectangle, SFormat);
        }

        private void OnTimePanelPaint(object sender, PaintEventArgs e)
        {
            SFormat = new StringFormat();
            SFormat.Alignment = StringAlignment.Near;
            SFormat.LineAlignment = StringAlignment.Center;

            contentFont = new Font(new FontFamily("Microsoft PhagsPa"), 12, FontStyle.Italic);
            textBrush = new SolidBrush(Color.FromArgb(52, 104, 192));
            stringRectangle = new Rectangle(Padding.Left*3/2, 0, timePanel.Width, timePanel.Height);

            e.Graphics.DrawString(time, contentFont, textBrush, stringRectangle, SFormat);
        }

        private void OnContentPanelPaint(object sender, PaintEventArgs e)
        {
            SFormat = new StringFormat();
            SFormat.Alignment = StringAlignment.Near;
            SFormat.LineAlignment = StringAlignment.Near;

            contentFont = new Font(new FontFamily("Microsoft PhagsPa"), 12);
            textBrush = new SolidBrush(Color.FromArgb(52, 104, 192));
            stringRectangle = new Rectangle(0, 0, contentPanel.Width, contentPanel.Height);

            contentMessage = GenerateContent(e.Graphics);

            SizeF contentSize = e.Graphics.MeasureString(contentMessage, contentFont);

            if (contentSize.Height>= Screen.PrimaryScreen.Bounds.Height)
            {
                seeMorePanel.Visible = true;
                isContentOutOfHeight = true;
            }
            else
            {
                seeMorePanel.Visible = false;
                isContentOutOfHeight = false;
            }
            e.Graphics.DrawString(contentMessage, contentFont, textBrush, stringRectangle, SFormat);
        }

        private void SplitContentToWords(string message)
        {
            string currentWord = "";
            for (int ctr = 0; ctr < message.Length; ctr++)
            {
                if(message[ctr]==' ' && currentWord!="")
                {
                    words.Add(currentWord);
                    currentWord = "";
                }
                else
                {
                    currentWord += message[ctr];
                }
            }

            if(currentWord!="")
            {
                words.Add(currentWord);
                currentWord = "";
            }
        }

        private string GenerateContent(Graphics g)
        {
            string actualContent = "", singleLineContent="" ;
            foreach(string Iter in words)
            {
                if(g.MeasureString(Iter, contentFont).Width >= contentPanel.Width)
                {
                    if(singleLineContent != "")
                    {
                        actualContent += singleLineContent + "\n";
                    }
                    float y = g.MeasureString(Iter, contentFont).Width;
                    int x = (int)(Iter.Length / (y / contentPanel.Width));
                    actualContent += Iter.Substring(0, x) + "\n";
                    singleLineContent = Iter.Substring(x) + " ";
                }
                else if(g.MeasureString(singleLineContent + Iter + " ", contentFont).Width>=contentPanel.Width)
                {
                    actualContent += singleLineContent+"\n";
                    singleLineContent = Iter + " ";
                }
                else
                {
                    singleLineContent += Iter + " ";
                }
            }
            if (singleLineContent != "")
                actualContent += singleLineContent;

            return actualContent;
        }

        private void seeMorePanel_Paint(object sender, PaintEventArgs e)
        {
            if(isContentOutOfHeight)
            {
                StringFormat S = new StringFormat();
                S.Alignment = StringAlignment.Center;
                S.LineAlignment = StringAlignment.Center;
                contentFont = new Font(new FontFamily("Microsoft PhagsPa"), 10, FontStyle.Bold);
                stringRectangle = new Rectangle(0, 0, seeMorePanel.Width, seeMorePanel.Height);
                string s = GenerateContent(e.Graphics);
                e.Graphics.DrawString("See More ^", contentFont, textBrush, stringRectangle, S);
            }
        }

        private void OnSeeMorePanelHover(object sender, EventArgs e)
        {
            if(isContentOutOfHeight)
            {
                textBrush = new SolidBrush(Color.Blue);
            }
        }

        private void OnSeeMorePanelEnter(object sender, EventArgs e)
        {
            if (isContentOutOfHeight)
            {
                textBrush = new SolidBrush(Color.Blue);
                seeMorePanel.Invalidate();
            }
        }

        private void OnSeeMorePanelLeave(object sender, EventArgs e)
        {
            if (isContentOutOfHeight)
            {
                textBrush = new SolidBrush(Color.FromArgb(52, 104, 192));
                seeMorePanel.Invalidate();
            }
        }

        private void OnFullContentViewed(object sender, EventArgs e)
        {
            isFullContentDisplayed = true;
            FullContentViewForm fullContentForm = new FullContentViewForm();
            fullContentForm.TitleName = header;
            fullContentForm.Content = message;
            //fullContentForm.WordArray = words;
            fullContentForm.StartPosition = FormStartPosition.Manual;
            fullContentForm.Size = new Size(600, 400);
            fullContentForm.Location = new Point((Screen.PrimaryScreen.Bounds.Width - 600)/2, (Screen.PrimaryScreen.Bounds.Height-400)/2);
            fullContentForm.Show();
        }

        private int borderRadius;
        private string header = "Header";
        private string message = "Message";
        private string contentMessage = "Message";
        private string time = "Time";
        private List<string> words = new List<string>();
        private Timer fadeOutTimer = new Timer();
        private Font headerFont, contentFont;
        private Pen linePen;
        private Brush textBrush;
        private Rectangle stringRectangle;
        private StringFormat SFormat;
    }
}
