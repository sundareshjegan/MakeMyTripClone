using System;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MakeMyTripClone
{
    public partial class ConfirmForm : Form
    {
        public ConfirmForm()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 60, 60));
            confirmBtn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, confirmBtn.Width, confirmBtn.Height, 10, 10));
            validityTimer.Interval = 1000;
            validityTimer.Tick += OnValidityTimerTicked;
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

        public bool IsVerified = false;
        private static readonly Random random = new Random();
        private Timer validityTimer = new Timer();
        private TimeSpan remainingTime = TimeSpan.FromMinutes(3);

        private int confirmationCode;

        private void OnValidityTimerTicked(object sender, EventArgs e)
        {
            remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
            if (remainingTime <= TimeSpan.Zero)
            {
                validityTimer.Stop();
                confirmationCode = GenerateConfirmationCode();
                remainingTime = TimeSpan.Zero;
                MessageBox.Show("Time's up!");
                Dispose();
            }
            timerLabel.Text = $"Code Expires in : {remainingTime:mm\\:ss}";
        }

        private void OnConfirmBtnClicked(object sender, EventArgs e)
        {
            int codeByUser = int.Parse(confirmationCodeTB.Text.Replace(" ", ""));
            if(confirmationCode == codeByUser)
            {
                Opacity /= 2;
                SuccessFailureForm success = new SuccessFailureForm("success", "Email Verifed Successfully");
                success.ShowDialog();
                IsVerified = true;
                Opacity *= 2;
                Dispose();
            }
            else
            {
                Opacity /= 2;
                SuccessFailureForm success = new SuccessFailureForm("failed", "Email Verification Failed");
                success.ShowDialog();
                validityTimer.Stop();
                IsVerified = false;
                Opacity *= 2;
            }
        }

        private void OnClosePBClicked(object sender, EventArgs e)
        {
            Dispose();
        }

        private void OnConfirmationCodeTBTextChanged(object sender, EventArgs e)
        {
            confirmBtn.Enabled = confirmationCodeTB.Text.Length == 11;
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
                emailLabel.Text = sub1 + sub2Stars + sub3 + "@" + arr[1];
            }
        }

        public bool SendEmail(string email, string name)
        {
            ConvertMaskedEmail(email);
            confirmationCode = GenerateConfirmationCode();

            MailAddress fromAddress = new MailAddress("whitehatsundar@gmail.com", "Make my Trip");
            MailAddress toAddress = new MailAddress(email);
            const string fromPassword = "xpre wkhu tgmh fsyj";
            string subject = "Email Confirmation - Make my Trip";

            #region Email Body
            string body = @"
            <html>
            <head>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        background-color: #f7f7f7; /* Light gray background */
                        margin: 0;
                        padding: 0;
                        font-size: 30px;
                    }
                    .container {
                        max-width: 600px;
                        margin: 20px auto; /* Added margin for centering */
                        padding: 20px;
                        background-color: #ffffff; /* White container background */
                        border-radius: 10px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    }
                    .header {
                        text-align: center;
                        padding-bottom: 20px;
                    }
                    .header img {
                        width: 300px;
                        height: auto;
                    }
                    .content {
                        padding: 20px;
                        background-color: #f9f9f9; /* Light gray content background */
                        border-radius: 8px;
                    }
                    .confirmation-code {
                        background-color: #42b983;
                        color: #ffffff;
                        padding: 10px;
                        border-radius: 5px;
                        font-weight: bold;
                    }
                    /* Increased the spacing between list items */
                    .content ul {
                        margin-top: 20px;
                        margin-bottom: 20px;
                        padding-left: 20px; /* Added left padding for list items */
                    }
                    .content ul li {
                        margin-bottom: 10px;
                    }
                    /* Styled the links */
                    a {
                        color: #007bff;
                        text-decoration: none;
                    }
                    a:hover {
                        text-decoration: underline;
                    }
                    /* Added some border radius to links */
                    a.btn {
                        display: inline-block;
                        padding: 10px 20px;
                        background-color: #007bff;
                        color: #ffffff;
                        border-radius: 5px;
                        text-decoration: none;
                        transition: background-color 0.3s;
                    }
                    a.btn:hover {
                        background-color: #0056b3;
                    }
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <iframe src='https://giphy.com/embed/LNjsnKKe83ZiriWcAs' width='480' height='180' frameBorder='0' class='giphy-embed' allowFullScreen></iframe>
<a href='https://giphy.com/stickers/mmt-makemytrip-myindia-LNjsnKKe83ZiriWcAs'></a>
                    </div>
                    <div class='content'>
                        <p>Hi " + name + @",</p>
                        <p>Welcome to Make My Trip! We're thrilled to have you onboard.</p>
                        <p>Make My Trip is your one-stop destination for all your travel needs. Whether you're planning a weekend getaway, a business trip, or a family vacation, we've got you covered.</p>
                        <p>Enter the following confirmation code to verify your email address:</p>
                        <p>Confirmation code: <span class='confirmation-code'>" + confirmationCode + @"</span></p>

                        <p>Here are a few things you can do next:</p>
                        <ul>
                            <li>Complete your profile to receive personalized recommendations.</li>
                            <li>Explore our app to discover exciting travel deals and destinations.</li>
                            <li>Book your next trip hassle-free with our easy-to-use booking platform.</li>
                        </ul>
                        <p>If you have any questions or need assistance, feel free to contact us at <a href='mailto:support@makemytrip.com'>support@makemytrip.com</a>.</p>
                        <p>Don't forget to follow us on <a href='https://instagram.com/sundaresh_jegan' class='btn'>Instagram</a> for the latest updates and promotions!</p>
                        <p>Once again, thank you for choosing Make My Trip. We look forward to serving you!</p>
                    </div>
                </div>
            </body>
            </html>";

            #endregion

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
                    validityTimer.Start();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                fromAddress = toAddress = null;
            }
        }

        private int GenerateConfirmationCode()
        {
            return random.Next(123000, 999999);
        }
        #endregion
    }
}
