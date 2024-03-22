using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeMyTripClone
{
    public partial class ConfirmationForm : Form
    {
        public ConfirmationForm()
        {
            InitializeComponent();
        }
        public ConfirmationForm(string email,string name)
        {
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 468, 462, 60, 60));
            InitializeComponent();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            ConvertMaskedEmail(email);
            SendEmail(email, name);
        }

        #region DLL for round Region
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

        private static readonly Random random = new Random();

        private TimeSpan remainingTime = TimeSpan.FromMinutes(3);
        Timer timer = new Timer();
        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1)); // Subtract 1 second from remaining time
            if (remainingTime <= TimeSpan.Zero)
            {
                timer.Stop();
                remainingTime = TimeSpan.Zero;
                MessageBox.Show("Time's up!");
            }
            timerLabel.Text = $"Code Expires in : {remainingTime:mm\\:ss}";
        }

        private void OnClosePBClicked(object sender, EventArgs e)
        {
            string text = confirmationCodeTB.Text.Replace(" ","");
            Dispose();
        }

        private void OnConfirmBtnClicked(object sender, EventArgs e)
        {

        }

        #region Helper Functions
        private void ConvertMaskedEmail(string email)
        {
            string[] arr = email.Split('@');
            if (arr[0].Length < 6)
            {
                emailLabel.Text = email;
            }
            else
            {
                string sub1 = arr[0].Substring(0, 3);
                string sub2Stars = new string('*', arr[0].Length - 5);
                string sub3 = arr[0].Substring(arr[0].Length - 2);

                emailLabel.Text = sub1 + " " + sub2Stars + " " + sub3 + "@" + arr[1];
            }
        }

        private void SendEmail(string email,string name)
        {
            MailAddress fromAddress = new MailAddress("whitehatsundar@gmail.com", "Sundareshwaran J");
            MailAddress toAddress = new MailAddress(email); 
            const string fromPassword = "xpre wkhu tgmh fsyj";
            string subject = "Email Confirmation - Make my Trip";
            string body1 = @"
                <html>
                <body>
                <center>
                    <img src='https://upload.wikimedia.org/wikipedia/commons/thumb/6/61/Makemytrip_logo.svg/800px-Makemytrip_logo.svg.png' alt='Logo' width='200' height='100'>
                    <p>Hi " + name + @",</p>
                    <p>Thank you for Joining Make My Trip.</p>
                    <p>Here is your confirmation code: <strong>" + GenerateConfirmationCode() + @"</strong></p>
                </center>   
                </body>
                </html>";
            string body = "Hi";
            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress.ToString(), toAddress.ToString())
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                    MessageBox.Show("Sent Successfully..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GenerateConfirmationCode()
        {
            return random.Next(123000, 999999);
        }

        #endregion
    }
}
