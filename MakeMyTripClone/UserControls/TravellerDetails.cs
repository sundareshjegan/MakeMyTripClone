using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MakeMyTripClone.UserControls
{
    public partial class TravellerDetails : UserControl
    {
        public TravellerDetails()
        {
            InitializeComponent();
            //maleBtn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, maleBtn.Width, maleBtn.Height, 10, 10));
            //panel1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel1.Width, panel1.Height, 20, 20));
        }
        
        public string TravellerName
        {
            get
            {
                return nameTB.Text;
            }
            set
            {
                nameWarningLabel.Text = nameTB.Text == "" ?  value : "";
            }
        }
        public string TravellerAge
        {
            get
            {
                return ageTB.Text;
            }
            set
            {
                ageWarningLabel.Text = ageTB.Text == "" ? value : "";
            }
        }
        public string SeatNumber
        {
            get
            {
                return seatNoLabel.Text;
            }
            set
            {
                seatNoLabel.Text = value;
            }
        }
        public string Gender
        {
            get => selectedGender;
        }

        private string selectedGender="";

        #region DLL to Create rounded Regions
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

        private void OnNameTBTextChanged(object sender, EventArgs e)
        {
            if (nameTB.Text == "")
            {
                namePanel.BackColor = Color.Red;
                nameWarningLabel.Text = "Name Required..!";
                return;
            }
            nameWarningLabel.Text = "";
            namePanel.BackColor = Color.Transparent;
        }

        private void OnAgeTBTextChanged(object sender, EventArgs e)
        {
            if (ageTB.Text == "")
            {
                agePanel.BackColor = Color.Red;
                ageWarningLabel.Text = "Age Required..!"; return;
            }
            if (int.Parse(ageTB.Text) > 200)
            {
                agePanel.BackColor = Color.Red;
                ageWarningLabel.Text = "Invalid Age..!"; return;
            }
            ageWarningLabel.Text = "";
            agePanel.BackColor = Color.Transparent;
        }
        private void OnAgeTBKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != '\b';
        }

        private void OnTextBoxActive(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                namePanel.BackColor = (textBox.Name == "nameTB") ? Color.DodgerBlue : Color.White;
                //namePanel.BackColor = (textBox.Text == "" && textBox.Name == "nameTB") ? Color.Red : Color.White;

                agePanel.BackColor = (textBox.Name == "ageTB") ? Color.DodgerBlue : Color.White;
                //agePanel.BackColor = (textBox.Text == "" && textBox.Name == "ageTB") ? Color.Red : Color.White;
            }
            else
            {
                agePanel.BackColor = namePanel.BackColor = Color.White;
            }
        }

        private void OnGenderBtnClicked(object sender, EventArgs e)
        {
            ResetButtonColors();
            Button button = sender as Button;
            button.FlatAppearance.BorderColor = Color.DodgerBlue;
            button.ForeColor = Color.DodgerBlue;
            button.BackColor = Color.FromArgb(200, 230, 255);
            selectedGender = button.Text;
        }
        private void ResetButtonColors()
        {
            femaleBtn.FlatAppearance.BorderColor = maleBtn.FlatAppearance.BorderColor = Color.Black;
            femaleBtn.ForeColor = maleBtn.ForeColor = Color.Black;
            femaleBtn.BackColor = maleBtn.BackColor = Parent.BackColor;
        }
    }
}
