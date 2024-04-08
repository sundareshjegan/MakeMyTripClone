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

namespace MakeMyTripClone
{
    public partial class PaymentForm : Form
    {
        public PaymentForm()
        {
            InitializeComponent();
            CreateCurves();
        }

        private Panel previousPanel = null;
        private Panel previousBlueBar = null;

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

        private void PaymentGatewayPanelClick(object sender, EventArgs e)
        {
            Panel currentPanel = sender as Panel;
            if (sender is Label label) currentPanel = label.Parent as Panel;
            if (sender is PictureBox pb) currentPanel = pb.Parent as Panel;

            #region Change TabControl Tabs
            amountDisplayPanel.Visible = true;
            paymentTabControl.Height = 350;
            if (currentPanel.Name == "bookNowPanel")
            {
                paymentTabControl.SelectedTab = bnplTab;
                amountDisplayPanel.Visible = false;
                paymentTabControl.Height = gateWaySelectionPanel.Height;
            }
            else if (currentPanel.Name == "upiPanel") paymentTabControl.SelectedTab = upiTab;
            else if (currentPanel.Name == "netBankingPanel")
            {
                paymentTabControl.SelectedTab = netBankingTab;
                amountDisplayPanel.Visible = false;
                paymentTabControl.Height = gateWaySelectionPanel.Height;
            }
            else if (currentPanel.Name == "googlePayPanel") paymentTabControl.SelectedTab = gPayTab;
            else if (currentPanel.Name == "creditCardPanel") paymentTabControl.SelectedTab = creditCardTab;
            #endregion

            if (previousPanel != null)
            {
                previousPanel.BackColor = Color.Transparent;
                previousBlueBar.Visible = false;
            }
            
            foreach (Control control in currentPanel.Controls)
            {
                if (control is Panel blueBar)
                {
                    blueBar.Visible = true;
                    previousBlueBar = blueBar;
                }  
            }
            currentPanel.BackColor = Color.White;
            previousPanel = currentPanel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Opacity /= 5;
            SuccessFailureForm success = new SuccessFailureForm("success", "Payment Successful!");
            success.ShowDialog();
            Opacity *= 5;
        }

        #region Helper Functions
        private void CreateCurves()
        {
            getAdditionalDiscountPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, getAdditionalDiscountPanel.Width, getAdditionalDiscountPanel.Height, 15, 15));
            rightPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, rightPanel.Width, rightPanel.Height, 15, 15));
            //customPanel10.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, customPanel10.Width, customPanel10.Height, 14, 14));
        }
        #endregion
    }
}
