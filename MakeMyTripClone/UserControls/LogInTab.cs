using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeMyTripClone
{
    public partial class LogInTab : UserControl
    {
        public LogInTab()
        {
            InitializeComponent();
            beforeLogInPanel.Visible = !isLoggedIn;
            afterLogInPanel.Visible = isLoggedIn;
        }
        private bool isLoggedIn;
        public bool IsLoggedIn
        {
            get
            {
                return isLoggedIn;
            }
            set
            {
                isLoggedIn = value;
                beforeLogInPanel.Visible = !isLoggedIn;
                afterLogInPanel.Visible = isLoggedIn;
            }
        }

        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                nameLabel.Text = value;
            }
        }
        private string userEmail;
        public string UserEmail
        {
            get
            {
                return userEmail;
            }
            set
            {
                userEmail = value;
                mailLabel.Text = value;
            }
        }
        private void OnLogInBtnClicked(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.ShowDialog();
        }
    }
}
