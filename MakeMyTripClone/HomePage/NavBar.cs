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
using System.Drawing.Drawing2D;

namespace MakeMyTripClone
{
    public partial class NavBar : UserControl
    {

        #region DLL for Curved Region
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

        #endregion

        #region DLL for button Click drop down event
        [DllImport("user32.dll")]

        private static extern bool PostMessage(
            IntPtr hWnd,  // handle to destination window
            Int32 msg,    // message
            Int32 wParam, // first message parameter
            Int32 lParam  // second message parameter
            );

        const Int32 WM_LBUTTONDOWN = 0x0201;
        #endregion

        public NavBar()
        {
            InitializeComponent();
            modesPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, modesPanel.Width, modesPanel.Height, 30, 30));
            detailPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, detailPanel.Width, detailPanel.Height, 30, 30));
            SearchButton.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, SearchButton.Width, SearchButton.Height, 30, 30));
            comboBox1.Text = "IND | ENG | INR";
            DateTime D = DateTime.Now;
            dateTimePicker.MinDate = D.Date;
            toComboBox.Text = "Chennai, Tamil Nadu";
            fromcomboBox.Text = "Coimbatore, Tamil Nadu";
            DBManager.OnUserLoggedIn += DBManagerOnUserLoggedIn;
            if (DBManager.IsUserLoggedIn)
            {
                logInTab.IsLoggedIn = DBManager.IsUserLoggedIn;
                logInTab.UserName = DBManager.CurrentUser.Name;
                logInTab.UserEmail = DBManager.CurrentUser.Email;
            }
            BusOnClick(this, EventArgs.Empty);
        }

        public static GraphicsPath GetRoundRectangle(Rectangle rectangle, int r)
        {
            int l = 2 * r;
            GraphicsPath gp = new GraphicsPath();
            gp.StartFigure();
            gp.AddLine(new Point(rectangle.X + r, rectangle.Y), new Point(rectangle.Right - r, rectangle.Y));
            gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Y, l, l), 270F, 90F);
            gp.AddLine(new Point(rectangle.Right, rectangle.Y + r), new Point(rectangle.Right, rectangle.Bottom - r));
            gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Bottom - l, l, l), 0F, 90F);
            gp.AddLine(new Point(rectangle.Right - r, rectangle.Bottom), new Point(rectangle.X + r, rectangle.Bottom));
            gp.AddArc(new Rectangle(rectangle.X, rectangle.Bottom - l, l, l), 90F, 90F);
            gp.AddLine(new Point(rectangle.X, rectangle.Bottom - r), new Point(rectangle.X, rectangle.Y + r));
            gp.AddArc(new Rectangle(rectangle.X, rectangle.Y, l, l), 180F, 90F);
            gp.CloseAllFigures();
            return gp;
        }


        private void DBManagerOnUserLoggedIn(object sender, bool isLoggedIn)
        {
            logInTab.IsLoggedIn = DBManager.IsUserLoggedIn;
            if (sender is CustomerDetails currentUser)
            {
                logInTab.UserName = currentUser.Name;
                logInTab.UserEmail = currentUser.Email;
            }
        }

        #region To and From Text box Change

        private void ToComboBoxtextChange(object sender, EventArgs e)
        {
            label11.Text = toComboBox.Text;

            if (label11.Text == label8.Text)
            {
                warningLabel.Visible = true;
            }
            else
            {
                warningLabel.Visible = false;
            }

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

        #endregion


        private void DateTimeValueChange(object sender, EventArgs e)
        {
            dateLabel.Text = dateTimePicker.Value.Day.ToString();

            string month = dateTimePicker.Value.ToString("MMMM");

            int year = dateTimePicker.Value.Year;

            year -= 2000;
            if (month.Length > 3)
            {
                monthyearLabel.Text = month.Remove(3) + "' " + year.ToString();
            }
            else
            {
                monthyearLabel.Text = month + "' " + year.ToString();
            }
            daylabel.Text = dateTimePicker.Value.DayOfWeek + "";
        }


        private void panel11_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Pen pen = new Pen(Color.Gray, 2);
            e.Graphics.DrawPath(pen, GetRoundRectangle(new Rectangle(0, 0, detailPanel.Width - 1, detailPanel.Height - 1), 15));
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
                BookingPageForm bookingpageform = new BookingPageForm();       

                String[] boarding = fromcomboBox.Text.Split(',');   // to get city places from string (Chennai,TamilNadu).......
                String[] destination = toComboBox.Text.Split(','); 

                // to check the server is reacheable or not to avoid null datas......
                if (bookingpageform.SetData(boarding[0], destination[0], dateTimePicker.Value.ToString("yyyy-MM-dd"), fromcomboBox, toComboBox, dateTimePicker))
                {
                    bookingpageform.ShowDialog(ParentForm);
                }

            }
        }

        private void BusOnClick(object sender, EventArgs e)
        {
            detailPanel.Visible = true;
            BusButton.BackgroundImage = Resources.busblue;
            SearchButton.Visible = true;
        }



    }
}
