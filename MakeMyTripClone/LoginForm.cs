using MakeMyTripClone.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeMyTripClone
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            CreateCurves();
        }

        private Random random = new Random();

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

        private void CreateCurves()
        {
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
            buttonsPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonsPanel.Width, buttonsPanel.Height, 30, 30));
            submitBtnPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, submitBtnPanel.Width, submitBtnPanel.Height, 30, 30));
            carouselPB.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, carouselPB.Width, carouselPB.Height, 30, 30));
            tabControl.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, tabControl.Width, tabControl.Height, 40, 40));

        }

        private void OnPasswordViewHideBtnClicked(object sender, EventArgs e)
        {
            passwordTB.UseSystemPasswordChar = !passwordTB.UseSystemPasswordChar;
            viewHideBtn.BackgroundImage = passwordTB.UseSystemPasswordChar ?
                                            Resources.viewPassword : Resources.hidePassword;
        }
        private void OnRegPwdViewHideBtnClicked(object sender, EventArgs e)
        {
            regPasswordTB.UseSystemPasswordChar = !regPasswordTB.UseSystemPasswordChar;
            regPwdViewHideBtn.BackgroundImage = regPasswordTB.UseSystemPasswordChar ?
                                            Resources.viewPassword : Resources.hidePassword;
        }

        private void OnSubmitBtnMouseHover(object sender, EventArgs e)
        {
            //to move the submit button if Input is not valid
            if (true)
            {
                // Move the button to a random position within the panel
                int newX = random.Next(signInRandomPanel.Width - submitBtnPanel.Width);
                int newY = random.Next(signInRandomPanel.Height - submitBtnPanel.Height);
                submitBtnPanel.Location = new Point(newX, newY);
            }
        }

        private void OnEmailTBTextChanged(object sender, EventArgs e)
        {
            bool isValidEmail = Regex.IsMatch(emailTB.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (isValidEmail)
            {

            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void signUpBtn_Click(object sender, EventArgs e)
        {
            registerInputPanel.Visible = true;
            loginInputPanel.Visible = false;
            tabControl.SelectedTab = registerTabPage;
        }

        private void signInBtn_Click(object sender, EventArgs e)
        {
            loginInputPanel.Visible = true;
            registerInputPanel.Visible = false;
            tabControl.SelectedTab = loginTabPage;
        }

        private void LoginForm_Resize(object sender, EventArgs e)
        {
            CreateCurves();
        }

        private void OnPasswordTBTextChanged(object sender, EventArgs e)
        {
            string password = regPasswordTB.Text;
            string strength = GetPasswordStrength(password);

            // Provide feedback to the user
            switch (strength)
            {
                case "Very Weak":
                    //passwordStrengthLabel.Text = "Very Weak Password";
                    passwordStrengthPanel.BackColor = passwordStrengthLabel.ForeColor = Color.OrangeRed;
                    passwordStrengthPanel.Width = 70;
                    break;
                case "Weak":
                    //passwordStrengthLabel.Text = "Weak Password";
                    passwordStrengthPanel.BackColor = passwordStrengthLabel.ForeColor = Color.Orange;
                    passwordStrengthPanel.Width = 140;
                    break;
                case "Medium":
                    //passwordStrengthLabel.Text = "Medium Password";
                    passwordStrengthPanel.BackColor = passwordStrengthLabel.ForeColor = Color.LightBlue;
                    passwordStrengthPanel.Width = 210;
                    break;
                case "Strong":
                    //passwordStrengthLabel.Text = "Strong Password";
                    passwordStrengthPanel.BackColor = passwordStrengthLabel.ForeColor = Color.LightGreen;
                    passwordStrengthPanel.Width = 280;
                    break;
            }
        }

        private string GetPasswordStrength(string password)
        {
            return "";
        }

        private void regMobileTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }
    }
}
