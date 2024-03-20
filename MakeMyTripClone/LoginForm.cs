using MakeMyTripClone.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        }

        private Random random = new Random();

        private void OnViewHideBtnClicked(object sender, EventArgs e)
        {
            passwordTB.UseSystemPasswordChar = !passwordTB.UseSystemPasswordChar;
            viewHideBtn.BackgroundImage = passwordTB.UseSystemPasswordChar ?
                                            Resources.viewPassword : Resources.hidePassword;
        }
        
        private void OnSubmitBtnMouseHover(object sender, EventArgs e)
        {
            //to move the submit button if Input is not valid
            if (true)
            {
                // Move the button to a random position within the panel
                int newX = random.Next(panel1.Width - submitBtn.Width);
                int newY = random.Next(panel1.Height - submitBtn.Height);
                submitBtn.Location = new Point(newX, newY);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void OnEmailTBTextChanged(object sender, EventArgs e)
        {
            bool isValidEmail = Regex.IsMatch(emailTB.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (isValidEmail)
            {

            }
        }
    }
}
