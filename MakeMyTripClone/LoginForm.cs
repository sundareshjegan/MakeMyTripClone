using MakeMyTripClone.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

        #region Button Events

        private void OnSignUpBtnClicked(object sender, EventArgs e)
        {
            tabControl.SelectedTab = registerTabPage;
        }
        private void OnSignInBtnClicked(object sender, EventArgs e)
        {
            tabControl.SelectedTab = loginTabPage;
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

        private void OnRegConPwdViewHideBtnClicked(object sender, EventArgs e)
        {
            regConPasswordTB.UseSystemPasswordChar = !regConPasswordTB.UseSystemPasswordChar;
            regConPwdViewHideBtn.BackgroundImage = regConPasswordTB.UseSystemPasswordChar ?
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

        
        private void OnRegisterBtnClicked(object sender, EventArgs e)
        {
            //if (BackEnd.UserAlreadyExists(regEmailTB.Text))
            //{
            //regEmailWarningLabel.Text = "Email-Id already Exists";
            //}
            //else
            //{

            //}
            //if(regNameTB.Text=="" || string.IsNullOrWhiteSpace(regNameTB.Text))
            //{
            //    regNameWarningLabel.Text = "Name should not be empty";
            //}
            //if(regEmailTB.Text=="" || string.IsNullOrWhiteSpace(regEmailTB.Text))
            //{
            //    regEmailWarningLabel.Text = "Email should not be empty";
            //}
            //if(regMobileTB.Text == "" || string.IsNullOrWhiteSpace(regMobileTB.Text))
            //{
            //    regMobileWarningLabel.Text = "Mobile Number Should Not be Empty";
            //}
            //else
            //{
                Opacity = Opacity/2;
                ConfirmForm confirmationForm = new ConfirmForm();
                confirmationForm.SendEmail(regEmailTB.Text,regNameTB.Text);
                confirmationForm.ShowDialog();
                Opacity = Opacity * 2;
            //}
        }

        #endregion

        #region Form Events
        private void OnLoginFormResized(object sender, EventArgs e)
        {
            CreateCurves();
        }
        #endregion

        #region TextBoxEvents
        private void OnEmailTBTextChanged(object sender, EventArgs e)
        {
            if (IsValidEmail(regEmailTB.Text))
            {
                regEmailValidPB.Image = Resources.validTick;
            }
            else
            {
                regEmailValidPB.Image = Resources.wrong;
            }
        }

        private void OnRegMobileTBTextChanged(object sender, EventArgs e)
        {
            regMobileValidPB.Image = regMobileTB.Text.Length == 10 ? Resources.validTick : Resources.wrong;
        }
        private void OnRegMobileTBKeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != '\b';
        }

        private void OnPasswordTBTextChanged(object sender, EventArgs e)
        {
            string password = regPasswordTB.Text;
            string strength = GetPasswordStrength(password).ToString();
            switch (strength)
            {
                case "VeryWeak":
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
                    passwordStrengthPanel.Width = emailUnderLinePanel.Width;
                    break;
            }
        }
        private void OnRegConPasswordTBTextChanged(object sender, EventArgs e)
        {
            if(regPasswordTB.Text != regConPasswordTB.Text && regPasswordTB.Text!="")
            {
                passwordCompareLabel.Text = "Passwords did not match..!";
            }
            else
            {
                passwordCompareLabel.Text = "";
            }
        }
        #endregion

        #region  Helper Functions
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }
        public enum PasswordStrength
        {
            VeryWeak,
            Weak,
            Medium,
            Strong
        }
        private PasswordStrength GetPasswordStrength(string password)
        {
            int score = 0;

            // Check length
            if (password.Length < 6)
                return PasswordStrength.VeryWeak;
            else if (password.Length < 8)
                score += 1;
            else if (password.Length < 10)
                score += 2;
            else
                score += 3;

            // Check for uppercase letters
            if (Regex.IsMatch(password, "[A-Z]"))
                score += 1;

            // Check for lowercase letters
            if (Regex.IsMatch(password, "[a-z]"))
                score += 1;

            // Check for numbers
            if (Regex.IsMatch(password, "[0-9]"))
                score += 1;

            // Check for special characters
            if (Regex.IsMatch(password, "[^a-zA-Z0-9]"))
                score += 1;

            // Categorize the strength based on the score
            if (score < 3)
                return PasswordStrength.Weak;
            else if (score < 5)
                return PasswordStrength.Medium;
            else
                return PasswordStrength.Strong;
        }

        private void CreateCurves()
        {
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
            buttonsPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonsPanel.Width, buttonsPanel.Height, 30, 30));
            submitBtnPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, submitBtnPanel.Width, submitBtnPanel.Height, 30, 30));
            registerBtnPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, registerBtnPanel.Width, registerBtnPanel.Height, 20, 20));
            carouselPB.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, carouselPB.Width, carouselPB.Height, 30, 30));
            tabControl.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, tabControl.Width, tabControl.Height, 40, 40));

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, 
                null, buttonsPanel, new object[] { true });
        }

        #endregion
    }
}
