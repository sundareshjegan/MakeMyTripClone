using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MakeMyTripClone.Properties;
using System.Runtime.InteropServices;

namespace MakeMyTripClone
{
    public partial class NavBar : UserControl
    {

        //Import

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

        [DllImport("user32.dll")]

        private static extern bool PostMessage(
            IntPtr hWnd, // handle to destination window
            Int32 msg, // message
            Int32 wParam, // first message parameter
            Int32 lParam // second message parameter
            );

        const Int32 WM_LBUTTONDOWN = 0x0201;
   
        public NavBar()
        {
            InitializeComponent();

            FlightBlue = Resources.airplane;

            FlightWhite = Resources.flight;

            HotelWhite = Resources.hotels;

            HotelBlue = Resources.hotelsblue;

            HomeWhite = Resources.homewhite;

            HomeBlue = Resources.homeblue;

            HolidaysWhite = Resources.holidayswhite;

            HolidaysBlue = Resources.holidaysblue;

            TrainBlue = Resources.trainblue;

            TrainWhite = Resources.trainwhite;

            BusWhite = Resources.buswhite;

            BusBlue = Resources.busblue;

            CabWhite = Resources.cabwhite;

            CabBlue = Resources.cabblue;

            ForexWhite = Resources.moneywhite;

            ForexBlue = Resources.moneyblue;

            InsuranceBlue = Resources.insuranceblue;

            InsuranceWhite = Resources.insurancewhite;

            panel10.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel10.Width, panel10.Height, 30, 30));

            panel11.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel11.Width, panel11.Height, 30, 30));

            SearchButton.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, SearchButton.Width, SearchButton.Height, 30, 30)); // Search

            //LoginButton.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, LoginButton.Width, LoginButton.Height, 30, 30));

            comboBox1.Text = "IND | ENG | INR";

           // DateTime D = DateTime.Now;

           // dateTimePicker.MinDate = D.Date ;

            toComboBox.Text = "Chennai,Tamil Nadu";

            fromcomboBox.Text = "Coimbatore,Tamil Nadu";


        }

        private Image FlightBlue, FlightWhite, HotelWhite, HotelBlue, HomeWhite, HomeBlue, HolidaysWhite, HolidaysBlue, TrainWhite, TrainBlue, BusWhite, BusBlue, CabWhite, CabBlue, ForexWhite, ForexBlue, InsuranceWhite, InsuranceBlue;

        private string ClickedTitle = "";

        //Text Change and Value Change

        private void ToComboBoxtextChange(object sender, EventArgs e)
        {
            label11.Text = toComboBox.Text;

            if(label11.Text==label8.Text)
            {
                warningLabel.Visible = true;
            }
            else
            {
                warningLabel.Visible = false;
            }
        }

        private void DateTimeValueChange(object sender, EventArgs e)
        {
            dateLabel.Text = dateTimePicker.Value.Day.ToString();

            int month = dateTimePicker.Value.Month;

            int year = dateTimePicker.Value.Year;

            string m = "";

            switch (month)
            {
                case 1:
                    m = "Jan";
                    break;

                case 2:
                    m = "Feb";
                    break;

                case 3:
                    m = "Mar";
                    break;

                case 4:
                    m = "Apr";
                    break;

                case 5:
                    m = "May";
                    break;
                case 6:
                    m = "Jun";
                    break;

                case 7:
                    m = "Jul";
                    break;

                case 8:
                    m = "Aug";
                    break;

                case 9:
                    m = "Sep";
                    break;

                case 10:
                    m = "Oct";
                    break;

                case 11:
                    m = "Nov";
                    break;

                default:
                    m = "Dec";
                    break;
            }

            year -= 2000;

            monthyearLabel.Text = m + "' " + year.ToString();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.ShowDialog();
        }

        private void NavBar_Load(object sender, EventArgs e)
        {
            BusOnClick(this,EventArgs.Empty);
        }

        private void FromComboBoxTextChange(object sender, EventArgs e)
        {
            label8.Text = fromcomboBox.Text;

            if (label11.Text == label8.Text)
            {
                warningLabel.Visible = true;
            }
            else
            {
                warningLabel.Visible = false;
            }
        }

        //Button Click (Bus)

        private void FromComboBoxClick(object sender, EventArgs e)
        {

            Int32 x = fromcomboBox.Width - 10;

            Int32 y = fromcomboBox.Height / 2;

            Int32 lParam = x + y * 0x00010000;

            PostMessage(fromcomboBox.Handle, WM_LBUTTONDOWN, 1, lParam);
        }

        private void ToComboBoxClick(object sender, EventArgs e)
        {
            Int32 x = toComboBox.Width - 10;

            Int32 y = toComboBox.Height / 2;

            Int32 lParam = x + y * 0x00010000;

            PostMessage(toComboBox.Handle, WM_LBUTTONDOWN, 1, lParam);
        }

        private void DateClick(object sender, EventArgs e)
        {
            Int32 x = dateTimePicker.Width - 10;

            Int32 y = dateTimePicker.Height / 2;

            Int32 lParam = x + y * 0x00010000;

            PostMessage(dateTimePicker.Handle, WM_LBUTTONDOWN, 1, lParam);
        }

        public void SearchButtonClick(object sender, EventArgs e)
        {
            if (warningLabel.Visible == false)
            {
                BookingPageForm page = new BookingPageForm();
                String[] boarding = fromcomboBox.Text.Split(',');
                String[] destination = toComboBox.Text.Split(',');
                page.SetData(boarding[0], destination[0], dateTimePicker.Value.ToString("yyyy-MM-dd"), fromcomboBox, toComboBox, dateTimePicker);
                page.ShowDialog();
            }
        }


        private void FlightOnClick(object sender, EventArgs e)
        {
            WhiteColourAll();

            BlueColourChange(FlightLabel, FlightButton, FlightBlue,FlightUnderLine);

        }
       
        private void HotelOnCLick(object sender, EventArgs e)
        {
            WhiteColourAll();

            BlueColourChange(HotelLabel, HotelButton, HotelBlue,HotelUnderLine);
        }

        private void HomeOnClick(object sender, EventArgs e)
        {
            WhiteColourAll();

            BlueColourChange(HomeLabel, HomeButton, HomeBlue,HomeUnderLine);
        }

        private void HolidaysOnClick(object sender, EventArgs e)
        {
            WhiteColourAll();

            BlueColourChange(HolidaysLabel, HolidaysButton, HolidaysBlue,HolidaysUnderLine);
        }

        private void TrainonClick(object sender, EventArgs e)
        {
            WhiteColourAll();

            BlueColourChange(TrainLabel, TrainButton, TrainBlue,TrainUnderLine);
        }

        private void BusOnClick(object sender, EventArgs e)
        {
            WhiteColourAll();

            panel11.Visible = true;

            SearchButton.Visible = true;

            BlueColourChange(BusLabel, BusButton, BusBlue,BusUnderLine);
        }

        private void CabOnClick(object sender, EventArgs e)
        {
            WhiteColourAll();

            BlueColourChange(CabLabel, CabButton, CabBlue,CabUnderLine);
        }

        private void InsuranceOnClick(object sender, EventArgs e)
        {
            WhiteColourAll();

            BlueColourChange(InsuranceLabel, InsuranceButton, InsuranceBlue, InsuranceUnderLine);
        }

        private void ForexOnClick(object sender, EventArgs e)
        {
            WhiteColourAll();

            BlueColourChange(ForexLabel, ForexButton, ForexBlue,ForexCurrencyUnderLine);
        }

        // Colour Change

        private void BlueColourChange(Label label,Button Button,Image Image,Label Underline)
        {
            label.ForeColor = Color.DodgerBlue;

            Button.BackgroundImage = Image;

           
            Font boldFont = new Font("Segoe UI", HotelLabel.Font.Size,FontStyle.Bold);

            label.Font = boldFont;

            Underline.Visible = true;

            ClickedTitle = label.Text;

        

        }

        private void WhiteColourChange(Label label,Button Button, Image Image,Label Underline)
        {
            Font Regular = new Font("Segoe UI", HotelLabel.Font.Size);

            label.ForeColor = Color.Black;

            Button.BackgroundImage = Image;

            label.Font = Regular;

            Underline.Visible = false;

            panel11.Visible = false;

            SearchButton.Visible = false;
        }

        private void WhiteColourAll()
        {
            WhiteColourChange(FlightLabel, FlightButton, FlightWhite,FlightUnderLine);

            WhiteColourChange(HotelLabel, HotelButton, HotelWhite,HotelUnderLine);

            WhiteColourChange(HomeLabel, HomeButton, HomeWhite,HomeUnderLine);

            WhiteColourChange(HolidaysLabel, HolidaysButton, HolidaysWhite,HolidaysUnderLine);

            WhiteColourChange(TrainLabel, TrainButton, TrainWhite,TrainUnderLine);

            WhiteColourChange(BusLabel, BusButton, BusWhite,BusUnderLine);

            WhiteColourChange(CabLabel, CabButton, CabWhite,CabUnderLine);

            WhiteColourChange(InsuranceLabel, InsuranceButton, InsuranceWhite, InsuranceUnderLine);

            WhiteColourChange(ForexLabel, ForexButton, ForexWhite,ForexCurrencyUnderLine);
        }

    }
}
