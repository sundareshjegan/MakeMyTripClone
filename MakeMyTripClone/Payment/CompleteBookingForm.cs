﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeMyTripClone.Payment
{
    public partial class CompleteBookingForm : Form
    {
        public CompleteBookingForm()
        {
            InitializeComponent();
        }

        private void OnTextBoxActive(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            emailTBPanel.BackColor = (textBox.Name == "emailTB") ? Color.DodgerBlue : Color.White;
            mobileTBPanel.BackColor = (textBox.Name == "mobileTB") ? Color.DodgerBlue : Color.White;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
