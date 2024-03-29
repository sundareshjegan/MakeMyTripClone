using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
            walletPB.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, walletPB.Width, walletPB.Height, 50, 50));
        }

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

        private void OnTextBoxActive(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            emailTBPanel.BackColor = (textBox.Name == "emailTB") ? Color.DodgerBlue : Color.White;
            mobileTBPanel.BackColor = (textBox.Name == "mobileTB") ? Color.DodgerBlue : Color.White;
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void OnClosePBClicked(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
