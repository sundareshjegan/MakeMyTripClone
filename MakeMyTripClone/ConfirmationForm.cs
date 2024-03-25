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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MakeMyTripClone
{
    public partial class ConfirmationForm : Form
    {
        public ConfirmationForm()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 468, 462, 60, 60));
            InitializeComponent();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
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
        private static int confirmationCode;
        private TimeSpan remainingTime = TimeSpan.FromMinutes(3);
        readonly Timer timer = new Timer();
        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1)); // Subtract 1 second from remaining time
            if (remainingTime <= TimeSpan.Zero)
            {
                timer.Stop();
                confirmationCode = -1;
                remainingTime = TimeSpan.Zero;
                MessageBox.Show("Time's up!");
            }
            timerLabel.Text = $"Code Expires in : {remainingTime:mm\\:ss}";
        }

        private void OnConfirmationCodeTBTextChanged(object sender, EventArgs e)
        {
            if (confirmationCodeTB.Text.Length==6)
            {
                MessageBox.Show("6 digits");
            }
            //confirmBtn.Enabled = confirmationCodeTB.Text.Length == 6;
        }
        private void OnClosePBClicked(object sender, EventArgs e)
        {
            Dispose();
        }

        private void OnConfirmBtnClicked(object sender, EventArgs e)
        {
            int codeByUser;
            string unmaskedText = confirmationCodeTB.Text.Replace(confirmationCodeTB.PromptChar.ToString(), "");


            if (int.TryParse(confirmationCodeTB.Text.Replace(" ", ""), out codeByUser))
            {
                if (codeByUser == confirmationCode)
                {
                    MessageBox.Show("Email Validated Successfully");
                }
                else
                {
                    MessageBox.Show("Invalid Confirmation Code");
                }
            }
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

        public bool SendEmail(string email,string name)
        {
            ConvertMaskedEmail(email);
            confirmationCode = GenerateConfirmationCode();

            MailAddress fromAddress = new MailAddress("whitehatsundar@gmail.com", "Make my Trip");
            MailAddress toAddress = new MailAddress(email); 
            const string fromPassword = "xpre wkhu tgmh fsyj";
            string subject = "Email Confirmation - Make my Trip";

            string body = @"
    <html>
    <head>
        <style>
            body {
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                margin: 0;
                padding: 0;
            }
            .container {
                max-width: 600px;
                margin: 0 auto;
                padding: 20px;
                background-color: #ffffff;
                border-radius: 10px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            }
            .header {
                text-align: center;
                padding-bottom: 20px;
            }
            .header img {
                width: 200px;
                height: auto;
            }
            .content {
                padding: 20px;
                background-color: #f9f9f9;
                border-radius: 8px;
            }
            .confirmation-code {
                background-color: #42b983;
                color: #ffffff;
                padding: 10px;
                border-radius: 5px;
            }
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='header'>
                <img src='https://upload.wikimedia.org/wikipedia/commons/thumb/6/61/Makemytrip_logo.svg/800px-Makemytrip_logo.svg.png' alt='Logo'>
            </div>
            <div class='content'>
                <p>Hi " + name + @",</p>
                <p>Welcome to Make My Trip! We're thrilled to have you onboard.</p>
                <p>Make My Trip is your one-stop destination for all your travel needs. Whether you're planning a weekend getaway, a business trip, or a family vacation, we've got you covered.</p>
                <p>Enter the following confirmation code to verify your email address..!</p>                
                <p>Confirmation code: <span class='confirmation-code'>" + confirmationCode + @"</span></p>

                <p>Here are a few things you can do next:</p>
                <ul>
                    <li>Complete your profile to receive personalized recommendations.</li>
                    <li>Explore our app to discover exciting travel deals and destinations.</li>
                    <li>Book your next trip hassle-free with our easy-to-use booking platform.</li>
                </ul>
                <p>If you have any questions or need assistance, feel free to contact us at support@makemytrip.com.</p>
                <p>Don't forget to follow us on <a href='https://www.facebook.com'>Facebook</a> and <a href='https://twitter.com/sundaresh_jegan'>Instagram</a> for the latest updates and promotions!</p>
                <p>Once again, thank you for choosing Make My Trip. We look forward to serving you!</p>
            </div>
        </div>
    </body>
    </html>";

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
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                    MessageBox.Show("Sent Successfully..!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private int GenerateConfirmationCode()
        {
            return random.Next(123000, 999999);
        }



        #endregion

        private void ConfirmationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
