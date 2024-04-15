//Sundareshwaran. J
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MakeMyTripClone
{
    public partial class CompleteBookingForm : Form
    {
        public CompleteBookingForm()
        {
            InitializeComponent();
            int noOfRatings = random.Next(53, 150);
            noOfRatingsLabel.Text = noOfRatings + " Ratings";
            double randomRating = Math.Round(random.NextDouble() * (4.9 - 3.5) + 3.5, 1);
            ratingLabel.Text = randomRating.ToString();
            DBManager.OnUserLoggedIn += OnDBManagerOnUserLoggedIn;
            //CreateCurves();
            //travellerDetailsPanel.Height = 0;
            //List<TravellerDetails> travellersList = new List<TravellerDetails>();
            //for (int i = 0; i < 2; i++)
            //{
            //    TravellerDetails traveller = new TravellerDetails();
            //    traveller.Dock = DockStyle.Top;
            //    travellerDetailsPanel.Height += traveller.Height+10;

            //    travellerDetailsPanel.Controls.Add(traveller);
            //    travellersList.Add(traveller);
            //}
            //foreach (TravellerDetails traveller in travellersList)
            //{
            //    traveller.BringToFront();
            //}
            //leftPanel.AutoScroll = true;
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

        Random random = new Random();
        List<TravellerDetails> travellersList;
        BookingDetails busDetails;
        private Timer adPanelTimer = new Timer();

        private void OnCompleteBookingFormLoad(object sender, EventArgs e)
        {
            rightSepPanel2.Visible = loginNowPanel.Visible = !DBManager.IsUserLoggedIn;
            rightSepPanel1.Visible = couponCodePanel.Visible = DBManager.IsUserLoggedIn;

            adPanel.Height = 0;
            adPanelTimer.Interval = 1000;
            adPanelTimer.Tick += OnAdPanelTimerTicked;
            adPanelTimer.Start();
        }

        private void OnAdPanelTimerTicked(object sender, EventArgs e)
        {
            //if (adPanel.Height > 200) adPanelTimer.Stop();
            adPanel.Height += 10;
        }

        private void OnDBManagerOnUserLoggedIn(object sender, bool e)
        {
            rightSepPanel2.Visible = loginNowPanel.Visible = !DBManager.IsUserLoggedIn;
            rightSepPanel1.Visible = couponCodePanel.Visible = DBManager.IsUserLoggedIn;
        }

        #region Click events
        private void OnClosePBClicked(object sender, EventArgs e)
        {
            Dispose();
        }
        private void OnAdClosePBClicked(object sender, EventArgs e)
        {
            closeAdLabel.Visible = adPB.Visible = adClosePB.Visible = false;
        }
        private void OnTravellerEditPBClicked(object sender, EventArgs e)
        {
            Dispose();
        }
        private void OnloginLabelClick(object sender, EventArgs e)
        {
            new LoginForm().ShowDialog();
        }
        private void OncouponCodeApplyLabelClick(object sender, EventArgs e)
        {
            int couponDiscountAmount = random.Next(1, 7) * 10;
            myDealAmountLabel.Text = (int.Parse(myDealAmountLabel.Text) + couponDiscountAmount).ToString();
            couponCodeApplyLabel.Enabled = false;
        }
        #endregion

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

        private void OncouponTBTextChanged(object sender, EventArgs e)
        {
            couponCodeApplyLabel.Enabled = true;
        }
        #endregion

        private void OnBaseFareAndCouponLabelTextChanged(object sender, EventArgs e)
        {
            totalAmountLabel.Text = int.Parse(baseFareLabel.Text) - int.Parse(myDealAmountLabel.Text) + "";
        }

        private void OnSecureTipCheckPBClicked(object sender, EventArgs e)
        {
            secureTipCheckPB.Image = secureTipCheckPB.Image == null ? Properties.Resources.validTick : null ;
            insurancePanel.Visible = secureTipCheckPB.Image != null;
            totalAmountLabel.Text = insurancePanel.Visible ? int.Parse(totalAmountLabel.Text) + 15 + "" : int.Parse(totalAmountLabel.Text) - 15+"";
        }

        public void SetData(BookingDetails busDetails)
        {
            this.busDetails = busDetails;
            int noOfTravellers = busDetails.Nooftravellers;
            travellersList = new List<TravellerDetails>();

            #region Adding Traveller Controls
            travellerDetailsPanel.Height = 0;
            string[] seatNumbers = busDetails.Bookedseatnumber.Split(',');
            for (int i = 0; i < noOfTravellers; i++)
            {
                TravellerDetails traveller = new TravellerDetails();
                traveller.SeatNumber = seatNumbers[i];
                traveller.Dock = DockStyle.Top;
                travellerDetailsPanel.Height += traveller.Height+10;

                travellerDetailsPanel.Controls.Add(traveller);
                travellersList.Add(traveller);
            }
            foreach (TravellerDetails traveller in travellersList)
            {
                traveller.BringToFront();
            }
            leftPanel.AutoScroll = true;
            #endregion
            seatNoLabel.Text = "Seat No : "+busDetails.Bookedseatnumber;
            busNameLabel.Text = busDetails.BusName;
            busTypeLabel.Text = "Bharat Benz/" + busDetails.Bustype;

            string[] timeAndDate = busDetails.Pickuptime.Split(' ');
            string pickUpTime = timeAndDate[0].Substring(0,5);
            string pickUpDate = timeAndDate[1] + " " + timeAndDate[2].Substring(0, 3);
            sourceTimeLabel.Text = pickUpTime;
            sourceDateLabel.Text = pickUpDate;

            timeAndDate = busDetails.Droptime.Split(' ');
            string dropTime = timeAndDate[0].Substring(0, 5);
            string dropDate = timeAndDate[1] + " " + timeAndDate[2].Substring(0, 3);
            destinationTimeLabel.Text = dropTime;
            destinationDateLabel.Text = pickUpDate;

            sourceCityLabel.Text = busDetails.Pickuplocation;
            destinationCityLabel.Text = busDetails.Droplocation;
            durationLabel.Text = busDetails.Durations;

            sourceBoardingLabel.Text = busDetails.Boardingpoint[1]+"\n"+busDetails.Boardingpoint[2]+"\n("+busDetails.Pickuplocation+")";
            destinationDepatureLabel.Text = busDetails.Droppoint[1] + "\n" + busDetails.Droppoint[2] + "\n(" + busDetails.Droplocation + ")";

            baseFareLabel.Text = busDetails.Totalamount.ToString();

            emailTB.Text = DBManager.IsUserLoggedIn ? DBManager.CurrentUser.Email : "";
        }

        private void OnContinueBtnClicked(object sender, EventArgs e)
        {
            if(IsAllDataEnteredAndValid())
            {
                if (DBManager.IsUserLoggedIn)
                {
                    PaymentForm paymentForm = new PaymentForm();
                    paymentForm.SetData(busDetails, travellersList, int.Parse(totalAmountLabel.Text), emailTB.Text);
                    paymentForm.ShowDialog();
                }
                else
                {
                    new LoginForm().ShowDialog();
                }
            }
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
            loginNowPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, loginNowPanel.Width, loginNowPanel.Height, 50, 50));
        }
        private bool IsAllDataEnteredAndValid()
        {
            foreach (TravellerDetails traveller in travellersList)
            {
                traveller.TravellerName = traveller.TravellerName == "" || string.IsNullOrWhiteSpace(traveller.TravellerName) ? "*Name required" : "";
                traveller.TravellerAge = traveller.TravellerAge == "" ? "*Age Required!" : "";
                traveller.TravellerAge = (int.Parse(traveller.TravellerAge) >=200 || int.Parse(traveller.TravellerAge) <=0) ? "Invalid Age!" : "";
                traveller.genderWarningLabel.Text = traveller.Gender == "" ? "Select Gender!" : "";
                emailWarningLabel.Text = emailTB.Text == "" ? "Email Id Required" : "";
                mobileWarningLabel.Text = mobileTB.Text == "" ? "Mobile No Required" : "";

                if (traveller.TravellerName == "") return false;
                if (traveller.TravellerAge == "") return false;
                if ((int.Parse(traveller.TravellerAge) >= 200) || (int.Parse(traveller.TravellerAge)<=0)) return false;
                if (traveller.Gender == "") return false;
            }
            stateWarningLabel.Text = stateCB.Text == "" ? "Select State" : "";
            if (stateCB.Text == "") return false;
            if (emailTB.Text == "") return false;
            if (!IsValidEmail(emailTB.Text)) return false;
            if (mobileTB.Text.Length != 10) return false;
            return true;
        }
        #endregion
    }
}
