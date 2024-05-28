﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MakeMyTripClone
{
    public partial class TravellerDetails : UserControl
    {
        public TravellerDetails()
        {
            InitializeComponent();
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

        public void ChangeButtonState(bool state)
        {
            maleBtn.Enabled = !state;
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
            if (int.Parse(ageTB.Text) > 200 || int.Parse(ageTB.Text) <= 0)
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

        private void OnNameTBKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != ' ';
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
            selectedGender = button.Text.Trim();
            genderWarningLabel.Text = "";
        }

        private void ResetButtonColors()
        {
            femaleBtn.FlatAppearance.BorderColor = maleBtn.FlatAppearance.BorderColor = Color.DarkGray;
            femaleBtn.ForeColor = maleBtn.ForeColor = Color.Black;
            femaleBtn.BackColor = maleBtn.BackColor = Parent.BackColor;
        }
    }
}
