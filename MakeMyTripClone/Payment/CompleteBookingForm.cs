using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MakeMyTripClone.UserControls;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MakeMyTripClone.Payment
{
    public partial class CompleteBookingForm : Form
    {
        public CompleteBookingForm()
        {
            InitializeComponent();
            //CreateCurves();
            //SetData();
            travellerDetailsPanel.Height = 0;
            List<TravellerDetails> travellersList = new List<TravellerDetails>();
            for (int i = 0; i < 3; i++)
            {
                TravellerDetails traveller = new TravellerDetails();
                traveller.Dock = DockStyle.Top;
                travellerDetailsPanel.Height += traveller.Height;

                travellerDetailsPanel.Controls.Add(traveller);
                travellersList.Add(traveller);
            }
            foreach (TravellerDetails traveller in travellersList)
            {
                traveller.BringToFront();
            }
            leftPanel.AutoScroll = true;
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

        private void OnClosePBClicked(object sender, EventArgs e)
        {
            Dispose();
        }

        #region TextBox Events
        private void OnTextBoxActive(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            emailTBPanel.BackColor = (textBox.Name == "emailTB") ? Color.DodgerBlue : Color.Transparent;
            mobileTBPanel.BackColor = (textBox.Name == "mobileTB") ? Color.DodgerBlue : Color.Transparent;
        }
        private void OnMobileTBKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != '\b';
        }

        private void OnMobileTBTextChanged(object sender, EventArgs e)
        {
            mobileTBPanel.BackColor = mobileTB.Text.Length != 10 ? Color.Red : Color.Transparent;
            mobileWarningLabel.Text = mobileTB.Text.Length != 10 ? "Invalid Mobile Number..!" : "";
        }

        private void OnEmailTBTextChanged(object sender, EventArgs e)
        {
            bool valid = IsValidEmail(emailTB.Text);
            emailTBPanel.BackColor = valid ? Color.Transparent : Color.Red;
            emailWarningLabel.Text = valid ? "" :"Invalid Email Id..!";
        }
        #endregion

        private void OnSecureTipCheckPBClicked(object sender, EventArgs e)
        {
            secureTipCheckPB.Image = secureTipCheckPB.Image == null ? Properties.Resources.validTick : null ;
        }

        private void SetData(RouteDetails route)
        {
            int noOfTravellers = 2;
            #region Adding Traveller Controls
            travellerDetailsPanel.Height = 0;
            List<TravellerDetails> travellersList = new List<TravellerDetails>();
            for (int i = 0; i < noOfTravellers; i++)
            {
                TravellerDetails traveller = new TravellerDetails();
                traveller.Dock = DockStyle.Top;
                travellerDetailsPanel.Height += traveller.Height;

                travellerDetailsPanel.Controls.Add(traveller);
                travellersList.Add(traveller);
            }
            foreach (TravellerDetails traveller in travellersList)
            {
                traveller.BringToFront();
            }
            leftPanel.AutoScroll = true;
            #endregion

            busNameLabel.Text = route.BusName;
            busTypeLabel.Text = route.BusType;
            sourceCityLabel.Text = route.Source;
            destinationCityLabel.Text = route.Destination;
            sourceTimeLabel.Text = route.StartTime;
            destinationTimeLabel.Text = route.EndTime;

        }
        #region Helper Functions
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }
        private void CreateCurves()
        {
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
            ratingPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, ratingPanel.Width, ratingPanel.Height, 7, 7));
            //walletPB.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, walletPB.Width, walletPB.Height, 50, 50));
        }
        #endregion
    }
}
