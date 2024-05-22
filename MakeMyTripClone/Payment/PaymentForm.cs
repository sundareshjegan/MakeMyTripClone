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
            DBManager.OnUserLoggedIn += DBManagerOnUserLoggedIn;
        }
 
        private Panel previousPanel = null;
        private Panel previousBlueBar = null;
        private List<SeatDetails> seatDetails = new List<SeatDetails>();
        private ETicket eticket;

        public static event EventHandler OnPaymentFormClosed;

        private void DBManagerOnUserLoggedIn(object sender, bool e)
        {
            throw new NotImplementedException();
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

        #region TextBox Events
        private void CardNumberTBClicked(object sender, EventArgs e)
        {
            int length = cardNumberTB.Text.TrimEnd().Length;
            cardNumberTB.SelectionStart = length;
        }
        private void OncvvNumberTBClicked(object sender, EventArgs e)
        {
            int length = cvvNumberTB.Text.TrimEnd().Length;
            cvvNumberTB.SelectionStart = length;
        }
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
            else
            {
                creditCardPanel.BackColor = Color.Transparent;
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

        #region PaymentButton Events
        private void OnBNPLProceedBtnClicked(object sender, EventArgs e)
        {
            Opacity -= 0.1;
            foreach (SeatDetails seat in seatDetails)
            {
                DBManager.ChangeSeatBookingState(seat);
            }
            eticket.PaymentMethod = "MakeMyTrip Book Now PayLater";
            SendMail();
        }
        private void OnUPIVerifyAndPayBtnClicked(object sender, EventArgs e)
        {
            if (upitext.Text != "" && upitext.Text.Any(char.IsDigit) && upitext.Text.Contains("@") && upitext.Text.Any(char.IsLetter))
            {
                upilabel.Visible = false;
                Opacity -= 0.1;
                foreach (SeatDetails seat in seatDetails)
                {
                    DBManager.ChangeSeatBookingState(seat);
                }
                eticket.PaymentMethod = "UPI";
                SendMail();
            }
            else
            {
                upilabel.Visible = true;
            }
        }
        private void OnGPayVerifyAndPayBtnClicked(object sender, EventArgs e)
        {
            if (gPayUpiTB.Text!="" && gupipintext.Text!="" && gPayUpiTB.Text.Any(char.IsDigit) && gPayUpiTB.Text.Contains("@") && gPayUpiTB.Text.Any(char.IsLetter))
            {
                UpiErrorlabel.Visible = false;
                Opacity -= 0.1;
                foreach (SeatDetails seat in seatDetails)
                {
                    DBManager.ChangeSeatBookingState(seat);
                }
                eticket.PaymentMethod = "Google Pay";
                SendMail();
            }
            else
            {
                UpiErrorlabel.Visible = true;
            }
        }
        private void OnCreditCardPayNowBtnClicked(object sender, EventArgs e)
        {
            #region validate credit card inputs
            if(cardNumberTB.Text == "" || cardNumberTB.Text.Length != 19)
            {
                creditCardWarningLabel.Text = "Invalid Card Number";
                return;
            }
            if(nameTB.Text == "")
            {
                creditCardWarningLabel.Text = "Enter Card Holder Name";
                return;
            }
            if(expiryMonthTB.Text=="" || expiryYearTB.Text == "")
            {
                creditCardWarningLabel.Text = "Invalid expiry date && month";
                return;
            }
            if(cvvNumberTB.Text.Length != 3)
            {
                creditCardWarningLabel.Text = "Invalid cvv number";
                return;
            }
            #endregion

            creditCardWarningLabel.Text = "Ticket Confirmation in process.. Please wait";
            Opacity -= 0.1;
            foreach (SeatDetails seat in seatDetails)
            {
                DBManager.ChangeSeatBookingState(seat);
            }
            eticket.PaymentMethod = "Credit - Card";
            SendMail();
           
        }
        #endregion
        private void SendMail()
        {
            LoaderForm loader = new LoaderForm();
            loader.Show();
            loader.Refresh(); // Force the form to update immediately

            // Send the e-ticket asynchronously
            Task.Run(() =>
            {
                ETicketGenerator eTicketGenerator = new ETicketGenerator();
                string ticketAsHTML = eTicketGenerator.GenerateETicket(eticket);
                eTicketGenerator.SendETicket(eticket.EmailToSendTicket, ticketAsHTML);
            })
            .ContinueWith(task =>
            {
                // Close the loader form after sending the e-ticket
                loader.Close();
                SuccessFailureForm success = new SuccessFailureForm("success", "Payment Successful! Thank you.Ticket Sent to your mail. Please keep it with your travel. \n Happy Journey..😊");
                success.ShowDialog();

                // Check for any errors during e-ticket sending
                if (task.IsFaulted)
                {
                    MessageBox.Show("Failed to send e-ticket: " + task.Exception?.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // Restore opacity
                Opacity += 0.1;
                Dispose();
                OnPaymentFormClosed?.Invoke(this, EventArgs.Empty);
            }, TaskScheduler.FromCurrentSynchronizationContext()); // Ensure the continuation runs on the UI thread
        }

        public void SetData(BookingDetails bookingDetails, List<TravellerDetails> travellers, int finalAmount, string emailToSendTicket)
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
            string[] seatStrings = seatNumbers.Select((number, index) => $"Seat {index + 1} - {number},\n").ToArray();
            seatDetailsLabel.Text = string.Join("", seatStrings);
            seatDetailsPanel.Height = seatDetailsLabel.Height+10;

            string[,] travellerDetails = new string[travellers.Count, 4];

            for (int i = 0; i < travellers.Count; i++)
            {
                travellerDetailsLabel.Text += $"{travellers[i].TravellerName} ({travellers[i].Gender} , {travellers[i].TravellerAge})\n";

                travellerDetails[i, 0] = travellers[i].TravellerName;
                travellerDetails[i, 1] = travellers[i].SeatNumber;
                travellerDetails[i, 2] = travellers[i].Gender;
                travellerDetails[i, 3] = travellers[i].TravellerAge.ToString();
            }
            travellersPanel.Height = travellerDetailsLabel.Height + 10;

            for (int i = 0; i < seatNumbers.Length; i++)
            {
                SeatDetails seat = new SeatDetails();
                seat.RouteId = bookingDetails.RootId;
                seat.SeatType = GetSeatType(seatNumbers[i], bookingDetails.Bustype);
                seat.SeatNumber = seatNumbers[i];
                seat.IsBooked = true;
                if (bookingDetails.Bustype.Contains("SL/ST"))
                {
                    int[] seatPrices = Array.ConvertAll(bookingDetails.SeatAmount.Split('/'), int.Parse);
                    seat.Price = GetSeatType(seatNumbers[i], bookingDetails.Bustype) == "ST" ? seatPrices[0] : seatPrices[1];
                }
                else
                {
                    seat.Price = int.Parse(bookingDetails.SeatAmount);
                }
                seat.CId = DBManager.CurrentUser.Id;
                seat.IsBookedByfemale = travellers[i].Gender.ToLower() == "female" ? true : false;
                seatDetails.Add(seat);
            }

            eticket = new ETicket()
            {
                BookingID = ""+bookingDetails.RootId + bookingDetails.BusId + DBManager.CurrentUser.Id,
                JourneyDate = pickUpDate,
                DepartureTime = pickUpTime,
                SourceCity = bookingDetails.Pickuplocation,
                DestinationCity = bookingDetails.Droplocation,
                BusName = bookingDetails.BusName + " " + bookingDetails.Bustype,
                Contact = "8608791884",
                PassengerDetails = travellerDetails,
                TotalFare = totalAmountDueLabel.Text,
                EmailToSendTicket = emailToSendTicket,
            };
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

        private void TextBox1TextChanged(object sender, EventArgs e)
        {
           TextBox t = sender as TextBox;
           if( t.Text.Contains("mobilenumber@upi"))
           {
                t.Text=t.Text.Remove(0, 16);
           }
        }

        private void UipintextKeyPress(object sender, KeyPressEventArgs e)
        {
           if(e.KeyChar!='\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
