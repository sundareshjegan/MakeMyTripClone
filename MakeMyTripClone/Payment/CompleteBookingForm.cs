//Sundareshwaran. J
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using CefSharp;
using CefSharp.WinForms;
namespace MakeMyTripClone
{
    public partial class CompleteBookingForm : Form
    {
        public CompleteBookingForm()
        {
            InitializeComponent();

            //randomly generate number of ratings for buses
            int noOfRatings = random.Next(53, 150);
            noOfRatingsLabel.Text = noOfRatings + " Ratings";

            //randomly generate star rating for buses
            double randomRating = Math.Round(random.NextDouble() * (4.9 - 3.5) + 3.5, 1);
            ratingLabel.Text = randomRating.ToString();

            //event triggered when an user log in to the system
            DBManager.OnUserLoggedIn += OnDBManagerOnUserLoggedIn;

            //event triggered when an user completes the payment successfully
            PaymentForm.OnPaymentFormClosed += OnPaymentFormClosed;

            //to iniitalize browser
           // Cef.Initialize(new CefSettings());
            #region comments
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
            #endregion
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
        
        private Random random = new Random();
        private List<TravellerDetails> travellersList;
        private BookingDetails busDetails;
        //private ChromiumWebBrowser browser;

        //When user already logged in or not
        private void OnCompleteBookingFormLoad(object sender, EventArgs e)
        {
            rightSepPanel2.Visible = loginNowPanel.Visible = !DBManager.IsUserLoggedIn;
            rightSepPanel1.Visible = couponCodePanel.Visible = DBManager.IsUserLoggedIn;
        }

        //if user log in from current page
        private void OnDBManagerOnUserLoggedIn(object sender, bool e)
        {
            rightSepPanel2.Visible = loginNowPanel.Visible = !DBManager.IsUserLoggedIn;
            rightSepPanel1.Visible = couponCodePanel.Visible = DBManager.IsUserLoggedIn;
            if(emailTB.Text == "")
            {
                emailTB.Text = DBManager.IsUserLoggedIn ? DBManager.CurrentUser.Email : "";
                emailTB.Enabled = false;
            }
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
        private void OnEmailEditPBClicked(object sender, EventArgs e)
        {
            emailTB.Enabled = true;
        }
        private void OncouponCodeApplyLabelClick(object sender, EventArgs e)
        {
            int couponDiscountAmount = random.Next(1, 7) * 10;
            if(couponTB.Text == "MAKEMYTRIP")
            {
                myDealAmountLabel.Text = (int.Parse(myDealAmountLabel.Text) + couponDiscountAmount).ToString();
                couponCodeApplyLabel.Enabled = false;
                couponTB.Enabled = false;
                couponWarningLabel.Text = "Coupon Applied";
            }
            else
            {
                couponWarningLabel.Text = "Invalid Coupon code!";
            }
        }
        //private void OnTermsAndConditionLabelClicked(object sender, EventArgs e)
        //{
        //    Form tc = new Form();
        //    tc.WindowState = FormWindowState.Maximized;
        //    tc.MinimizeBox = tc.MaximizeBox = false;
        //    browser = new ChromiumWebBrowser("https://promos.makemytrip.com/Bus/index.html");
        //    tc.Controls.Add(browser);
        //    browser.Dock = DockStyle.Fill;
        //    tc.ShowDialog();
        //    browser.Dispose();
        //    tc.Dispose();
        //}
        private void OnUserAgreementLabelClicked(object sender, EventArgs e)
        {
            //Form tc = new Form();
            //tc.WindowState = FormWindowState.Maximized;
            //tc.MinimizeBox = tc.MaximizeBox = false;
            //browser = new ChromiumWebBrowser("https://www.makemytrip.com/legal/user_agreement.html");
            //tc.Controls.Add(browser);
            //browser.Dock = DockStyle.Fill;
            //tc.ShowDialog();
            //browser.Dispose();
            //tc.Dispose();
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
            policytimelabel.Text = busDetails.Pickuptime;
            #region Adding Traveller Controls
            travellerDetailsPanel.Height = 0;
            string[] seatNumbers = busDetails.Bookedseatnumber.Split(',');
            for (int i = 0; i < noOfTravellers; i++)
            {
                TravellerDetails traveller = new TravellerDetails();
                traveller.SeatNumber = seatNumbers[i];
                traveller.Dock = DockStyle.Top;
                travellerDetailsPanel.Height += traveller.Height;

                travellerDetailsPanel.Controls.Add(traveller);
                traveller.ChangeButtonState(busDetails.FemaleSeatList[i]);
                travellersList.Add(traveller);
            }
            travellerDetailsPanel.Height += 20;
            foreach (TravellerDetails traveller in travellersList)
            {
                traveller.BringToFront();
            }
            leftPanel.AutoScroll = true;
            #endregion

            #region Set UI data
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
            headerDateTimeLabel.Text = sourceCityLabel.Text + " to " + destinationCityLabel.Text + " | " + sourceDateLabel.Text + " | " + sourceTimeLabel.Text;
            emailTB.Text = DBManager.IsUserLoggedIn ? DBManager.CurrentUser.Email : "";
            #endregion
        }

        private void OnContinueBtnClicked(object sender, EventArgs e)
        {
            if (IsAllDataEnteredAndValid())
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

        private void OnPaymentFormClosed(object sender, EventArgs e)
        {
            foreach(Control c in Controls)
            {
                c.Dispose();
            }
            travellersList.Clear();
            Dispose();
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
                if(traveller.TravellerAge!="")
                traveller.TravellerAge = (int.Parse(traveller.TravellerAge) >=200 || int.Parse(traveller.TravellerAge) <=0) ? "Invalid Age!" : "";
                traveller.genderWarningLabel.Text = traveller.Gender == "" ? "Select Gender!" : "";
                emailWarningLabel.Text = emailTB.Text == "" ? "Email Id Required" : "";
                mobileWarningLabel.Text = mobileTB.Text == "" ? "Mobile No Required" : "";

                if (traveller.TravellerName == "") return false;
                if (traveller.TravellerAge == "") return false;
                if (traveller.TravellerAge != "")
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

        private void ViewPoliciesLabelMouseEnter(object sender, EventArgs e)
        {
            policypanel.Visible = true;
            amtlabel.Text =baseFareLabel.Text;
            firstlabel.Text = int.Parse(baseFareLabel.Text) - 50 + "";
            secondlabel.Text = int.Parse(baseFareLabel.Text) - 100 + "";
            Thirdlabel.Text = int.Parse(baseFareLabel.Text) - 150 + "";
            Fourthlabel.Text = int.Parse(baseFareLabel.Text) - 200 + "";
            fifthlabel.Text = int.Parse(baseFareLabel.Text) - 250 + "";
            sixlabel.Text = int.Parse(baseFareLabel.Text) - 300 + "";
            policypanel.Location = new Point(viewPoliciesLabel.Location.X+700, viewPoliciesLabel.Height + 50);
        }

        private void ViewPoliciesLabelMouseLeave(object sender, EventArgs e)
        {
            policypanel.Visible = false;
        }
    }
}
