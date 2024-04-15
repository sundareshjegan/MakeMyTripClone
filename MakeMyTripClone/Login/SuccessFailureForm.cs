using MakeMyTripClone.Properties;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MakeMyTripClone
{
    public partial class SuccessFailureForm : Form
    {
        public SuccessFailureForm(string status, string message)
        {
            InitializeComponent();
            this.message = message;
            if (status.ToLower() == "success")
            {
                gifImage = Resources.successTick;
                msgLabel.ForeColor = Color.DodgerBlue;
                msgLabel.Text = message;
            }
            else
            {
                gifImage = Resources.loaderFailed;
                msgLabel.ForeColor = Color.Red;
            }
            #region animation code
            //gifImage = new Bitmap("D:\\Sundareshwaran\\C_Sharp_Projects\\WindowsForms\\ProfileManagement\\greenPurple.gif");
            animationTimer = new Timer();
            animationTimer.Interval = 30;
            if(status == "success")
            {
                animationTimer.Tick += SuccessAnimationTimer;
            }
            else
            {
                animationTimer.Tick += FailureAnimationTimer;
            }
            totalFrames = gifImage.GetFrameCount(System.Drawing.Imaging.FrameDimension.Time);
            animationTimer.Start();
            #endregion
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }
        #region animation varialbles and events
        private Image gifImage;
        private Timer animationTimer;
        private int currentFrame;
        private int totalFrames;

        private string message;

        private void SuccessAnimationTimer(object sender, EventArgs e)
        {
            currentFrame++;// = (currentFrame + 1) % totalFrames;
            if (currentFrame >= totalFrames)
            {
                animationTimer.Stop(); // Stop the timer when all frames have been displayed
            }
            else
            {
                Invalidate();
            }
        }

        private void FailureAnimationTimer(object sender, EventArgs e)
        {
            currentFrame++;
            if (currentFrame >= totalFrames - 40)//to pause after x is displayed
            {
                animationTimer.Stop(); // Stop the timer when all frames have been displayed
                msgLabel.Text = message;
            }
            else
            {
                Invalidate();
            }

        }

        private void SuccessForm_Paint(object sender, PaintEventArgs e)
        {
            if (gifImage != null)
            {
                gifImage.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Time, currentFrame);
                e.Graphics.DrawImage(gifImage, ClientRectangle);
            }
        }

        #endregion

        #region DLL to create curve regions
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );
        #endregion

        private void closePB_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
