using MakeMyTripClone.Booking;
using MakeMyTripClone.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeMyTripClone
{
    public partial class BookingPageForm : Form
    {
        public BookingPageForm()
        {
            InitializeComponent();
            DBManager.OnUserLoggedIn += DBManagerOnUserLoggedIn;
            if (DBManager.IsUserLoggedIn)
            {
                logInTab1.IsLoggedIn = DBManager.IsUserLoggedIn;
                logInTab1.UserName = DBManager.CurrentUser.Name;
                logInTab1.UserEmail = DBManager.CurrentUser.Email;
            }
            PaymentForm.OnPaymentFormClosed += OnPaymentCompletedAndFormClosed;
        }

        private void OnPaymentCompletedAndFormClosed(object sender, EventArgs e)
        {
            DBManager.OnUserLoggedIn -= DBManagerOnUserLoggedIn;
            PaymentForm.OnPaymentFormClosed -= OnPaymentCompletedAndFormClosed;
            //busesList.Clear();
            //droppoints.Clear();
            //boardingpoints.Clear();
            //traveloperatorpoints.Clear();
            //buses.Clear();
            //filterlist.Clear();
            Dispose();
        }

        private Color colour = SystemColors.GradientInactiveCaption, gray = Color.DimGray, highlight = SystemColors.Highlight, white = Color.White;
        private bool isTime, isDtime, isDropup, isPick, isDrop, isTravel, isPickup, isprevcheck, istravelcheck, isdpcheck;
        private int no = 0;
        private Buses bus;
        public static List<RouteDetails> busesList = new List<RouteDetails>();
        public static List<object> droppoints = new List<object>();
        public static List<object> boardingpoints = new List<object>();
        public static List<string> traveloperatorpoints = new List<string>();
        private List<Buses> buses = new List<Buses>();
        private List<Buses> filterlist = new List<Buses>();
        private string isAc = null, seatType = null, pickTime = null, dropTime = null, pickPoint = null, travelpoint = null, dropPoint = null;
        CustomCheckBox1 prevcheck = null;
        CustomCheckBox2 travelcheck = null;
        CustomCheckBox3 dpcheck = null;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private void DBManagerOnUserLoggedIn(object sender, bool isLoggedIn)
        {
            logInTab1.IsLoggedIn = DBManager.IsUserLoggedIn;
            if (sender is CustomerDetails currentUser)
            {
                logInTab1.UserName = currentUser.Name;
                logInTab1.UserEmail = currentUser.Email;
            }
        }

        private void SrchbuttonClick(object sender, EventArgs e)
        {
            String[] boarding = fromcomboBox.Text.Split(',');
            String[] destination = tocomboBox.Text.Split(',');
            busesList.Clear();
            droppoints.Clear();
            boardingpoints.Clear();
            traveloperatorpoints.Clear();
            adducpanel.Controls.Clear();
            buses.Clear();
            SetData(boarding[0], destination[0], departdateTimePicker.Value.ToString("yyyy-MM-dd"), null, null, null);
            isTravel = false;
            isPickup = false;
            isDropup = false;
            PanelChanges(travelvaluepanel);
            PanelChanges(dpvaluepanel);
            PanelChanges(pickupvaluepanel);
        }

        public void SetData(string boarding, string destination, string date, ComboBox from, ComboBox to, DateTimePicker dateTime)
        {
            busesList = DBManager.GetBuses(boarding, destination, date);
            fromcomboBox.Text = boarding;
            tocomboBox.Text = destination;
            departdateTimePicker.Text = date;
            if (from != null && to != null)
            {
                foreach (var items in from.Items)
                {
                    fromcomboBox.Items.Add(items);
                }
                foreach (var items in to.Items)
                {
                    tocomboBox.Items.Add(items);
                }
            }

            boardingpoints = DBManager.GetBoardingPoints(boarding, destination, date);
            droppoints = DBManager.GetDropPoints(boarding, destination, date);
            traveloperatorpoints = DBManager.GetTravel(boarding, destination, date);
            BusButton.BackgroundImage = Resources.busblue;
            AddDatas();
        }

        private void AddDatas()
        {
            srchbutton.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, srchbutton.Width, srchbutton.Height, 30, 30));
            for (int i = 0; i < busesList.Count; i++)
            {
                bus = new Buses();
                adducpanel.Controls.Add(bus);
                bus.Dock = DockStyle.Top;
                buses.Add(bus);
                bus.Setdata(i, fromcomboBox.Text, tocomboBox.Text, departdateTimePicker.Value.ToString("yyyy-MM-dd"));
            }
            busesfoundlabel.Text = BusesFound() + " Buses Found";
        }

        private void CloseSearch()
        {
            foreach (var bus in buses)
            {
                bus.Srch();
            }
        }

        private void PanelsClick(object sender, EventArgs e)
        {
            CloseSearch();
            Panel panel = sender as Panel;
            if (panel.BackColor == colour)
            {
                panel.BackColor = white;
                no--;
                if (no <= 0)
                {
                    clearallbutton.ForeColor = gray;
                    no = 0;
                }
                putimeclearbutton.ForeColor = gray;
                ddtimeclrbutton.ForeColor = gray;
                if (panel.Name == "acpanel" || panel.Name == "nonacpanel") isAc = null;
                if (panel.Name == "sleeperpanel" || panel.Name == "seaterpanel") seatType = null;
                if (panel.Name == "putimesrpanel" || panel.Name == "putimengtpanel" || panel.Name == "putimesspanel" || panel.Name == "putimeevepanel")
                {
                    pickTime = null;
                    isPick = false;
                }
                if (panel.Name == "ddsrpanel" || panel.Name == "ddsspanel" || panel.Name == "ddngtpanel" || panel.Name == "ddevepanel")
                {
                    dropTime = null;
                    isDrop = false;
                }
                CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
                Filter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
            }
            else
            {
                clearallbutton.ForeColor = SystemColors.Highlight;
                no++;
                if (panel.Name == "acpanel" && nonacpanel.BackColor == colour)
                {
                    nonacpanel.BackColor = Color.White;
                    isAc = "AC";
                    CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
                    isAc = "NON";

                }
                else if (panel.Name == "nonacpanel" && acpanel.BackColor == colour)
                {
                    acpanel.BackColor = Color.White;
                    isAc = "NON";
                    CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
                    isAc = "AC";

                }
                if (panel.Name == "sleeperpanel" && seaterpanel.BackColor == colour)
                {
                    seaterpanel.BackColor = Color.White;
                    seatType = "SL";
                    CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
                    seatType = "ST";
                }
                else if (panel.Name == "seaterpanel" && sleeperpanel.BackColor == colour)
                {
                    sleeperpanel.BackColor = Color.White;
                    seatType = "ST";
                    CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
                    seatType = "SL";
                }
                if (panel.Name == "acpanel") isAc = "AC";
                if (panel.Name == "nonacpanel") isAc = "NON";
                if (panel.Name == "sleeperpanel") seatType = "SL";
                if (panel.Name == "seaterpanel") seatType = "ST";
                if (panel.Name == "putimesrpanel")
                {
                    putimeevepanel.BackColor = Color.White;
                    putimesspanel.BackColor = Color.White;
                    putimengtpanel.BackColor = Color.White;
                    putimeclearbutton.ForeColor = SystemColors.Highlight;
                    pickTime = putimesrlabel.Tag + "";
                    if (isPick) CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
                    isPick = true;
                }
                else if (panel.Name == "putimeevepanel")
                {
                    putimesrpanel.BackColor = Color.White;
                    putimesspanel.BackColor = Color.White;
                    putimengtpanel.BackColor = Color.White;
                    putimeclearbutton.ForeColor = SystemColors.Highlight;
                    pickTime = putimeevelabel.Tag + "";
                    if (isPick) CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
                    isPick = true;
                }
                else if (panel.Name == "putimesspanel")
                {
                    putimesrpanel.BackColor = Color.White;
                    putimeevepanel.BackColor = Color.White;
                    putimengtpanel.BackColor = Color.White;
                    putimeclearbutton.ForeColor = SystemColors.Highlight;
                    pickTime = putimesslabel.Tag + "";
                    if (isPick) CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
                    isPick = true;
                }
                else if (panel.Name == "putimengtpanel")
                {
                    putimesrpanel.BackColor = Color.White;
                    putimeevepanel.BackColor = Color.White;
                    putimesspanel.BackColor = Color.White;
                    putimeclearbutton.ForeColor = SystemColors.Highlight;
                    pickTime = putimengtlabel.Tag + "";
                    if (isPick) CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
                    isPick = true;
                }
                if (panel.Name == "ddsrpanel")
                {
                    ddevepanel.BackColor = Color.White;
                    ddsspanel.BackColor = Color.White;
                    ddngtpanel.BackColor = Color.White;
                    ddtimeclrbutton.ForeColor = SystemColors.Highlight;
                    dropTime = ddsrlabel.Tag + "";
                    if (isDrop) CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
                    isDrop = true;
                }
                else if (panel.Name == "ddevepanel")
                {
                    ddsrpanel.BackColor = Color.White;
                    ddsspanel.BackColor = Color.White;
                    ddngtpanel.BackColor = Color.White;
                    ddtimeclrbutton.ForeColor = SystemColors.Highlight;
                    dropTime = ddevelabel.Tag + "";
                    if (isDrop) CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
                    isDrop = true;
                }
                else if (panel.Name == "ddsspanel")
                {
                    ddevepanel.BackColor = Color.White;
                    ddsrpanel.BackColor = Color.White;
                    ddngtpanel.BackColor = Color.White;
                    ddtimeclrbutton.ForeColor = SystemColors.Highlight;
                    dropTime = ddsslabel.Tag + "";
                    if (isDrop) CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
                    isDrop = true;
                }
                else if (panel.Name == "ddngtpanel")
                {
                    ddevepanel.BackColor = Color.White;
                    ddsspanel.BackColor = Color.White;
                    ddsrpanel.BackColor = Color.White;
                    ddtimeclrbutton.ForeColor = SystemColors.Highlight;
                    dropTime = ddngtlabel.Tag + "";
                    if (isDrop) CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
                    isDrop = true;
                }
                panel.BackColor = colour;
                Filter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
            }
        }

        private void CustomFilter(string isAc, string seatType, string picktime, string droptime, string pickpoint, string travel, string droppoint)
        {
            if (isAc == null)
            {
                foreach (var bus in buses)
                {
                    if (bus.SeatType == seatType) bus.Visible = true;
                }
            }
            if (seatType == null)
            {
                foreach (var bus in buses)
                {
                    if (bus.BusType == isAc) bus.Visible = true;
                }
            }
            if (travel == null)
            {
                foreach (var bus in buses)
                {
                    if (isAc != null && seatType != null)
                    {
                        if (bus.BusType == isAc && bus.SeatType == seatType) bus.Visible = true;
                        else bus.Visible = false;
                    }
                    else
                    {
                        if (bus.BusType == isAc || bus.SeatType == seatType) bus.Visible = true;
                        else bus.Visible = false;
                    }
                }
            }
            if (pickpoint == null)
            {
                foreach (var bus in buses)
                {
                    if (isAc != null && seatType != null)
                    {
                        if (bus.BusType == isAc && bus.SeatType == seatType) bus.Visible = true;
                        else bus.Visible = false;
                    }
                    else if (isAc != null || seatType != null)
                    {
                        if (bus.BusType == isAc || bus.SeatType == seatType) bus.Visible = true;
                        else bus.Visible = false;
                    }
                    else Reset();
                }
            }
            if (droppoint == null)
            {
                foreach (var bus in buses)
                {
                    if (isAc != null && seatType != null)
                    {
                        if (bus.BusType == isAc && bus.SeatType == seatType) bus.Visible = true;
                        else bus.Visible = false;
                    }
                    else if (isAc != null || seatType != null)
                    {
                        if (bus.BusType == isAc || bus.SeatType == seatType) bus.Visible = true;
                        else bus.Visible = false;
                    }
                    else Reset();
                }
            }
            if (picktime == null)
            {
                foreach (var bus in buses)
                {
                    if (bus.BusType == isAc && bus.SeatType == seatType) bus.Visible = true;
                }
            }
            else
            {
                Reset();
            }
            if (droptime == null)
            {
                foreach (var bus in buses)
                {
                    if (bus.BusType == isAc && bus.SeatType == seatType) bus.Visible = true;
                }
            }
            else
            {
                Reset();
            }
            busesfoundlabel.Text = BusesFound() + " Buses Found";
        }

        private void Filter(string isAc, string seatType, string picktime, string droptime, string pickpoint, string travel, string droppoint)
        {
            if (clearallbutton.ForeColor == gray)
            {
                Reset();
            }
            if (isAc != null)
            {
                foreach (var bus in buses)
                {
                    if (bus.BusType == isAc && bus.Visible) bus.Visible = true;
                    else bus.Visible = false;
                }
            }
            if (seatType != null)
            {
                foreach (var bus in buses)
                {
                    if (bus.SeatType == seatType && bus.Visible) bus.Visible = true;
                    else bus.Visible = false;
                }
            }
            if (picktime != null)
            {
                string[] time = picktime.Split(' ');
                foreach (var bus in buses)
                {
                    if (Convert.ToDateTime(time[1]) < Convert.ToDateTime(time[0]) && (Convert.ToDateTime(bus.StartTime) >= Convert.ToDateTime(time[0]) || Convert.ToDateTime(bus.StartTime) < Convert.ToDateTime(time[1])) && bus.Visible == true) bus.Visible = true;
                    else if (Convert.ToDateTime(bus.StartTime) >= Convert.ToDateTime(time[0]) && Convert.ToDateTime(bus.StartTime) < Convert.ToDateTime(time[1]) && bus.Visible == true) bus.Visible = true;
                    else bus.Visible = false;
                }
            }
            if (droptime != null)
            {
                string[] time = droptime.Split(' ');
                foreach (var bus in buses)
                {
                    if (Convert.ToDateTime(time[1]) < Convert.ToDateTime(time[0]) && (Convert.ToDateTime(bus.EndTime) >= Convert.ToDateTime(time[0]) || Convert.ToDateTime(bus.EndTime) < Convert.ToDateTime(time[1])) && bus.Visible == true) bus.Visible = true;
                    else if (Convert.ToDateTime(bus.EndTime) >= Convert.ToDateTime(time[0]) && Convert.ToDateTime(bus.EndTime) < Convert.ToDateTime(time[1]) && bus.Visible == true) bus.Visible = true;
                    else bus.Visible = false;
                }
            }
            if (pickpoint != null)
            {
                foreach (var bus in buses)
                {
                    int c = 0;
                    foreach (var point in bus.Boarding)
                    {
                        if (point.Contains(pickpoint) && bus.Visible) bus.Visible = true;
                        else c++;
                    }
                    if (c == bus.Boarding.Count) bus.Visible = false;
                }
            }
            if (droppoint != null)
            {
                foreach (var bus in buses)
                {
                    int c = 0;
                    foreach (var point in bus.Droping)
                    {
                        if (point.Contains(droppoint) && bus.Visible) bus.Visible = true;
                        else c++;
                    }
                    if (c == bus.Droping.Count) bus.Visible = false;
                }
            }
            if (travel != null)
            {
                foreach (var bus in buses)
                {
                    if (bus.BusName == travel && bus.Visible) bus.Visible = true;
                    else bus.Visible = false;
                }
            }
            busesfoundlabel.Text = BusesFound() + " Buses Found";
        }

        private void LabelsClick(object sender, EventArgs e)
        {
            Label label = sender as Label;
            PanelsClick(label.Parent, EventArgs.Empty);
        }

        private void Reset()
        {
            foreach (var bus in buses)
            {
                bus.Visible = true;
            }
        }

        private void PictureboxesClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            PanelsClick(pictureBox.Parent, EventArgs.Empty);
        }

        private void ClearallbuttonClick(object sender, EventArgs e)
        {
            if (clearallbutton.ForeColor == highlight)
            {
                isAc = null;
                seatType = null;
                Reset();
                no = 0;
                pickTime = null;
                dropTime = null;
                isDrop = false;
                isPick = false;
                pickPoint = null;
                dropPoint = null;
                travelpoint = null;
                acpanel.BackColor = white;
                nonacpanel.BackColor = white;
                sleeperpanel.BackColor = white;
                seaterpanel.BackColor = white;
                putimesrpanel.BackColor = white;
                putimeevepanel.BackColor = white;
                putimesspanel.BackColor = white;
                putimengtpanel.BackColor = white;
                ddsrpanel.BackColor = white;
                ddevepanel.BackColor = white;
                ddsspanel.BackColor = white;
                ddngtpanel.BackColor = white;
                clearpickuppointbutton.ForeColor = gray;
                putimeclearbutton.ForeColor = gray;
                travelclrbutton.ForeColor = gray;
                dpclrbutton.ForeColor = gray;
                ddtimeclrbutton.ForeColor = gray;
                seatersleepercheckBox.Checked = false;
                clearallbutton.ForeColor = gray;
                foreach (CustomCheckBox1 c in pickupvaluepanel.Controls)
                {
                    if (c.BackColor == colour) c.Colourchange();
                }
                foreach (CustomCheckBox2 c in travelvaluepanel.Controls)
                {
                    if (c.BackColor == colour) c.Colourchange();
                }
                foreach (CustomCheckBox3 c in dpvaluepanel.Controls)
                {
                    if (c.BackColor == colour) c.Colourchange();
                }
                busesfoundlabel.Text = BusesFound() + " Buses Found";
            }
        }

        private void DpclrbuttonClick(object sender, EventArgs e)
        {
            CloseSearch();
            PutimeclearbuttonClick(null, EventArgs.Empty);
            DdtimeclrbuttonClick(null, EventArgs.Empty);
            dpclrbutton.ForeColor = gray;
            foreach (CustomCheckBox1 c in dpvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    c.Colourchange();
                    no--;
                }
            }
            dropPoint = null;
            CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
            if (no <= 0) clearallbutton.ForeColor = gray;
        }

        private void ClearpickuppointbuttonClick(object sender, EventArgs e)
        {
            CloseSearch();
            PutimeclearbuttonClick(null, EventArgs.Empty);
            DdtimeclrbuttonClick(null, EventArgs.Empty);
            clearpickuppointbutton.ForeColor = gray;
            foreach (CustomCheckBox1 c in pickupvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    c.Colourchange();
                    no--;
                }
            }
            pickPoint = null;
            isprevcheck = true;
            CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
            if (no <= 0) clearallbutton.ForeColor = gray;
        }

        private void TravelclrbuttonClick(object sender, EventArgs e)
        {
            CloseSearch();
            PutimeclearbuttonClick(null, EventArgs.Empty);
            DdtimeclrbuttonClick(null, EventArgs.Empty);
            travelclrbutton.ForeColor = gray;
            foreach (CustomCheckBox1 c in travelvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    c.Colourchange();
                    no--;
                }
            }
            travelpoint = null;
            istravelcheck = true;
            CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
            if (no <= 0) clearallbutton.ForeColor = gray;
        }

        private void PutimeclearbuttonClick(object sender, EventArgs e)
        {
            CloseSearch();
            putimesrpanel.BackColor = white;
            putimeevepanel.BackColor = white;
            putimesspanel.BackColor = white;
            putimengtpanel.BackColor = white;
            putimeclearbutton.ForeColor = gray;
            pickTime = null;
            isdpcheck = true;
            isPick = false;
            CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
            no--;
            if (no <= 0) clearallbutton.ForeColor = gray;
        }

        private void DdtimeclrbuttonClick(object sender, EventArgs e)
        {
            CloseSearch();
            ddsrpanel.BackColor = white;
            ddevepanel.BackColor = white;
            ddsspanel.BackColor = white;
            ddngtpanel.BackColor = white;
            ddtimeclrbutton.ForeColor = gray;
            dropTime = null;
            isDrop = false;
            CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
            no--;
            if (no <= 0) clearallbutton.ForeColor = gray;
        }

        private void SeatersleepercheckBoxCheckedChanged(object sender, EventArgs e)
        {
            CloseSearch();
            if (seatersleepercheckBox.Checked)
            {
                seatType = "ST";
                no++;
            }
            else
            {
                seatType = null;
                no--;
                CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
            }
            if (no <= 0) clearallbutton.ForeColor = gray;
            else clearallbutton.ForeColor = highlight;
            Filter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
        }

        private void FstbuttonClick(object sender, EventArgs e)
        {
            if (fstbutton.BackColor == colour)
            {
                fstbutton.BackColor = white;
            }
            else
            {
                fstbutton.BackColor = colour;
                arbutton.BackColor = white;
                dprbutton.BackColor = white;
                var fastest = buses.OrderBy(item => TimeSpan.Parse(item.Duration)).ToList();
                foreach (var bus in fastest)
                {
                    bus.Dock = DockStyle.Top;
                    bus.BringToFront();
                }
            }
        }

        private void LoginButtonClick(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.ShowDialog();
        }

        private void SwappictureBoxClick(object sender, EventArgs e)
        {
            string s = fromcomboBox.Text;
            fromcomboBox.Text = tocomboBox.Text;
            tocomboBox.Text = s;
        }

        private void ArbuttonClick(object sender, EventArgs e)
        {
            if (arbutton.BackColor == colour)
            {
                arbutton.BackColor = white;
            }
            else
            {
                DateTime targetTime = DateTime.Now;
                fstbutton.BackColor = white;
                arbutton.BackColor = colour;
                dprbutton.BackColor = white;
                var arrival = buses.OrderBy(bus =>
                {
                    TimeSpan duration = Convert.ToDateTime(bus.StartTime) - targetTime;
                    return duration.TotalMinutes;
                }).ThenBy(bus => bus.StartTime).ToList();
                foreach (var bus in arrival)
                {
                    bus.Dock = DockStyle.Top;
                    bus.BringToFront();
                }
            }
        }

        private void ComboBoxTextChanged(object sender, EventArgs e)
        {
            string[] ss = fromcomboBox.Text.Split(',');
            fromcomboBox.Text = ss[0];
            string[] s = tocomboBox.Text.Split(',');
            tocomboBox.Text = s[0];
            if (fromcomboBox.Text == tocomboBox.Text) warningLabel.Visible = true;
            else warningLabel.Visible = false;
        }

        private int BusesFound()
        {
            adducpanel.Controls.Add(nobuspanel);
            int count = 0;
            foreach (var bus in buses)
            {
                if (bus.Visible == true) count++;
            }
            if (count == 0)
            {
                nobuspanel.Visible = true;
            }
            else
            {
                nobuspanel.Visible = false;
            }
            return count;
        }

        private void DprbuttonClick(object sender, EventArgs e)
        {
            if (dprbutton.BackColor == colour)
            {
                dprbutton.BackColor = white;
            }
            else
            {
                fstbutton.BackColor = white;
                arbutton.BackColor = white;
                dprbutton.BackColor = colour;
                DateTime targetTime = DateTime.Now;
                var departure = buses.OrderBy(bus =>
                {
                    TimeSpan duration = Convert.ToDateTime(bus.EndTime) - targetTime;
                    return duration.TotalMinutes;
                }).ThenBy(bus => bus.EndTime).ToList();
                foreach (var bus in departure)
                {
                    bus.Dock = DockStyle.Top;
                    bus.BringToFront();
                }
            }
        }

        private void PickupClick(object sender, EventArgs e)
        {
            if (pickupvaluepanel.Visible == true)
            {
                pickupvaluepanel.Visible = false;
                pupointpictureBox.Image = Resources.down;
            }
            else Locations(pickupvaluepanel, pupointpictureBox, boardingpoints, ref isPickup, 1);
        }

        private void TPictureBoxClick(object sender, EventArgs e)
        {
            List<object> listOfObjects = traveloperatorpoints.Select(s => (object)s).ToList();
            if (travelvaluepanel.Visible == true)
            {
                travelvaluepanel.Visible = false;
                travelpictureBox.Image = Resources.down;
            }
            else Locations(travelvaluepanel, travelpictureBox, listOfObjects, ref isTravel, 2);
        }

        private void PanelChanges(Panel p)
        {
            p.Controls.Clear();
        }

        private void DropPointClick(object sender, EventArgs e)
        {
            if (dpvaluepanel.Visible == true)
            {
                dpvaluepanel.Visible = false;
                dppictureBox.Image = Resources.down;
            }
            else Locations(dpvaluepanel, dppictureBox, droppoints, ref isDropup, 3);
        }

        private void DtimeClick(object sender, EventArgs e)
        {
            if (putimevaluepanel.Visible == true)
            {
                putimevaluepanel.Visible = false;
                putimepictureBox.Image = Resources.down;
            }
            else Time(ref isTime, putimevaluepanel, putimepictureBox);
        }

        private void DTime(object sender, EventArgs e)
        {
            if (ddtimevaluepanel.Visible == true)
            {
                ddtimevaluepanel.Visible = false;
                ddtimepictureBox.Image = Resources.down;
            }
            else Time(ref isDtime, ddtimevaluepanel, ddtimepictureBox);
        }

        private void Locations(Panel p, PictureBox pict, List<object> li, ref bool bb, int n)
        {
            pict.Image = Resources.arrow_up;
            p.Visible = true;
            if (!bb)
            {
                for (int i = 0; i < li.Count; i++)
                {
                    Control checkBoxes = null;
                    if (n == 1)
                    {
                        checkBoxes = new CustomCheckBox1();
                        p.Controls.Add(checkBoxes);
                        (checkBoxes as CustomCheckBox1).Dock = DockStyle.Top;
                        string[] ss = li[i].ToString().Split('&');
                        if (ss.Length == 1)
                        {
                            (checkBoxes as CustomCheckBox1).SetValuesCheckbox(ss[0]);
                        }
                        else (checkBoxes as CustomCheckBox1).SetValuesCheckbox(ss[1]);
                        (checkBoxes as CustomCheckBox1).checks1 += CheckBoxeschecks;
                    }
                    if (n == 2)
                    {
                        checkBoxes = new CustomCheckBox2();
                        p.Controls.Add(checkBoxes);
                        (checkBoxes as CustomCheckBox2).Dock = DockStyle.Top;
                        string[] ss = li[i].ToString().Split('&');
                        if (ss.Length == 1)
                        {
                            (checkBoxes as CustomCheckBox2).SetValuesCheckbox(ss[0]);
                        }
                        else (checkBoxes as CustomCheckBox2).SetValuesCheckbox(ss[1]);
                        (checkBoxes as CustomCheckBox2).checks2 += BookingPageFormchecks2; ;
                    }
                    if (n == 3)
                    {
                        checkBoxes = new CustomCheckBox3();
                        p.Controls.Add(checkBoxes);
                        (checkBoxes as CustomCheckBox3).Dock = DockStyle.Top;
                        string[] ss = li[i].ToString().Split('&');
                        if (ss.Length == 1)
                        {
                            (checkBoxes as CustomCheckBox3).SetValuesCheckbox(ss[0]);
                        }
                        else (checkBoxes as CustomCheckBox3).SetValuesCheckbox(ss[1]);
                        (checkBoxes as CustomCheckBox3).checks3 += BookingPageFormchecks3; ;
                    }
                }

                bb = true;
            }

        }

        private void BookingPageFormchecks3(object sender, EventArgs e)
        {
            CloseSearch();
            dropPoint = null;
            foreach (CustomCheckBox3 c in dpvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    dropPoint = c.checkBoxs.Text;
                    dpclrbutton.ForeColor = highlight;
                    if (dpcheck == c && dpcheck != null && !isdpcheck)
                    {
                        c.Colourchange();
                        dropPoint = null;
                        dpcheck = null;
                        isdpcheck = true;
                        no--;
                        dpclrbutton.ForeColor = gray;
                    }
                    else
                    {
                        isdpcheck = false;
                        dpclrbutton.ForeColor = highlight;
                        no++;
                    }
                    if (dpcheck != c && dpcheck != null) no--;
                    dpcheck = c;
                }
                else
                {
                    c.SetCheckedState();
                }
            }
            CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
            Filter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
            if (dpclrbutton.ForeColor == highlight) clearallbutton.ForeColor = highlight;
            else clearallbutton.ForeColor = highlight;

        }

        private void BookingPageFormchecks2(object sender, EventArgs e)
        {
            CloseSearch();
            travelpoint = null;
            foreach (CustomCheckBox2 c in travelvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    travelclrbutton.ForeColor = highlight;
                    travelpoint = c.checkBoxs.Text;
                    if (travelcheck == c && travelcheck != null && !istravelcheck)
                    {
                        c.Colourchange();
                        travelpoint = null;
                        travelcheck = null;
                        istravelcheck = true;
                        no--;
                        travelclrbutton.ForeColor = gray;
                    }
                    else
                    {
                        istravelcheck = false;
                        travelclrbutton.ForeColor = highlight;
                        no++;
                    }
                    if (travelcheck != c && travelcheck != null) no--;
                    travelcheck = c;
                }
                else
                {
                    c.SetCheckedState();
                }
            }
            CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
            Filter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
            if (travelclrbutton.ForeColor == highlight) clearallbutton.ForeColor = highlight;
            else clearallbutton.ForeColor = highlight;

        }

        private void CheckBoxeschecks(object sender, EventArgs e)
        {
            CloseSearch();
            pickPoint = null;
            foreach (CustomCheckBox1 c in pickupvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    pickPoint = c.checkBoxs.Text;
                    clearpickuppointbutton.ForeColor = highlight;
                    if (prevcheck == c && prevcheck != null && !isprevcheck)
                    {
                        c.Colourchange();
                        pickPoint = null;
                        prevcheck = null;
                        isprevcheck = true;
                        no--;
                        clearpickuppointbutton.ForeColor = gray;
                    }
                    else
                    {
                        isprevcheck = false;
                        clearpickuppointbutton.ForeColor = highlight;
                        no++;
                    }
                    if (prevcheck != c && prevcheck != null) no--;
                    prevcheck = c;

                }
                else
                {
                    c.SetCheckedState();

                }
            }
            CustomFilter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
            Filter(isAc, seatType, pickTime, dropTime, pickPoint, travelpoint, dropPoint);
            if (clearpickuppointbutton.ForeColor == highlight) clearallbutton.ForeColor = highlight;
            else clearallbutton.ForeColor = highlight;
        }

        private void Time(ref bool b, Panel p, PictureBox picture)
        {
            if (!b)
            {
                picture.Image = Resources.arrow_up;
                p.Visible = true;
                b = true;
            }
            else
            {
                picture.Image = Resources.down;
                p.Visible = false;
                b = false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            foreach (Control c in Controls)
            {
                RemoveAndDisposeControls(c);
            }
        }
        private void RemoveAndDisposeControls(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Controls.Count > 0)
                {
                    RemoveAndDisposeControls(c);
                }
                c.Dispose();
            }
            control.Controls.Clear();
        }
    }
}
