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
        private List<SeatDeatils> seatDetails = new List<SeatDeatils>();

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

        private void OnPayButtonClicked(object sender, EventArgs e)
        {
            Opacity -= 0.1;
            foreach(SeatDeatils seat in seatDetails)
            {
                DBManager.ChangeSeatBookingState(seat);
            }
            SuccessFailureForm success = new SuccessFailureForm("success", "Payment Successful!");
            success.ShowDialog();
            Opacity += 0.1;
        }

        public void SetData(BookingDetails bookingDetails, List<TravellerDetails> travellers, int finalAmount)
        {
            busNameLabel.Text = bookingDetails.BusName;
            busTypeLabel.Text = bookingDetails.Bustype;

            string[] timeAndDate = bookingDetails.Pickuptime.Split(' ');
            string pickUpTime = timeAndDate[0].Substring(0, 5);
            string pickUpDate = timeAndDate[1] + " " + timeAndDate[2].Substring(0, 3);
            sourceTimeLabel.Text = pickUpTime;
            sourceDateLabel.Text = pickUpDate;

            timeAndDate = bookingDetails.Droptime.Split(' ');
            string dropTime = timeAndDate[0].Substring(0, 5);
            string dropDate = timeAndDate[1] + " " + timeAndDate[2].Substring(0, 3);
            destinationTimeLabel.Text = dropTime;
            destinationDateLabel.Text = pickUpDate;

            sourceCityLabel.Text = bookingDetails.Pickuplocation;
            destinationCityLabel.Text = bookingDetails.Droplocation;
            durationLabel.Text = bookingDetails.Durations;

            pickUpLocationLabel.Text = bookingDetails.Boardingpoint[1] + "\n" + bookingDetails.Boardingpoint[2];
            dropLocationLabel.Text = bookingDetails.Droppoint[1] + "\n" + bookingDetails.Droppoint[2];

            baseFareLabel.Text = bookingDetails.Totalamount.ToString();
            othersAmountLabel.Text = Math.Abs(finalAmount - bookingDetails.Totalamount).ToString();
            totalAmountLabel.Text = finalAmount.ToString();
            totalAmountDueLabel.Text = finalAmount.ToString();


            string[] seatNumbers = bookingDetails.Bookedseatnumber.Split(',');
            string[] seatStrings = seatNumbers.Select((number, index) => $"Seat {index + 1} - {number}\n").ToArray();
            seatDetailsLabel.Text = string.Join(", ", seatStrings);
            seatDetailsPanel.Height = seatDetailsLabel.Height+10;
            
            foreach(TravellerDetails traveller in travellers)
            {
                travellerDetailsLabel.Text += $"{traveller.TravellerName} ({traveller.Gender} , {traveller.TravellerAge})\n";
            }
            travellersPanel.Height = travellerDetailsLabel.Height + 10;

            for (int i = 0; i < seatNumbers.Length; i++)
            {
                SeatDeatils seat = new SeatDeatils();
                seat.RouteId = bookingDetails.RootId;
                seat.SeatType = GetSeatType(seatNumbers[i], bookingDetails.Bustype);
                seat.SeatNumber = seatNumbers[i];
                seat.IsBooked = true;
                if (bookingDetails.Bustype.Contains("SL/ST"))
                {
                    int[] seatPrices = Array.ConvertAll(bookingDetails.seatAmount.Split('/'), int.Parse);
                    seat.Price = GetSeatType(seatNumbers[i], bookingDetails.Bustype) == "ST" ? seatPrices[0] : seatPrices[1];
                }
                else
                {
                    seat.Price = int.Parse(bookingDetails.seatAmount);
                }
                //seat.CId = DBManager.LoggedInUserID;
                seatDetails.Add(seat);
            }
        }
        #region Helper Functions
        private void CreateCurves()
        {
            getAdditionalDiscountPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, getAdditionalDiscountPanel.Width, getAdditionalDiscountPanel.Height, 15, 15));
            yourBookingPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, yourBookingPanel.Width, yourBookingPanel.Height, 15, 15));
            //customPanel10.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, customPanel10.Width, customPanel10.Height, 14, 14));
        }
        private string GetSeatType(string seatNumber,string busType)
        {
            if (busType.Contains("SL/ST"))
            {
                if (seatNumber == "1" || seatNumber == "6" || seatNumber == "11" || seatNumber == "16" || seatNumber == "21")
                {
                    return "SL";
                }
                else if(seatNumber.ToLower().Contains("u"))
                {
                    return "SL";
                }
                else
                {
                    return "ST";
                }
            }
            else if(busType.Contains("SL") && !busType.Contains("SL/"))
            {
                return "SL";
            }
            return "ST";
        }
        #endregion
    }
}
