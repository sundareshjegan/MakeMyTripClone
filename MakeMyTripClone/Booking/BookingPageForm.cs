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
            Dispose();
        }

        private Color colour = SystemColors.GradientInactiveCaption, gray = Color.DimGray, highglight = SystemColors.Highlight, white = Color.White;
        private bool isfalse, isTime, isDtime, isNUll, isTravel, isPick, isDrop, isDrops, isADD, isNot;
        private int no = 0;
        private Buses bus;
        public static List<RouteDetails> busesList = new List<RouteDetails>();
        public static List<object> droppoints = new List<object>();
        public static List<object> boardingpoints = new List<object>();
        public static List<string> traveloperatorpoints = new List<string>();
        private List<Buses> buses = new List<Buses>();
        private List<Buses> filterlist = new List<Buses>();
        private string isAc = null, seatType = null, pickTime = null, dropTime = null, pickPoint = null, travel = null, dropPoint = null;

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
            SetData(boarding[0], destination[0], departdateTimePicker.Value.ToString("yyyy-MM-dd"), null, null, null);
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
            endofbuslabel.Dock = DockStyle.Top;
            endofbuslabel.BringToFront();
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
                if (panel.Name == "acpanel" || panel.Name == "nonacpanel") isAc =null;
                if (panel.Name == "sleeperpanel" || panel.Name == "seaterpanel") seatType = null;
                if (panel.Name == "putimesrpanel" || panel.Name == "putimengtpanel" || panel.Name == "putimesspanel" || panel.Name == "putimeevepanel")
                {
                    pickTime = null;
                    isPick = false;
                }
                if(panel.Name== "ddsrpanel" || panel.Name== "ddsspanel" || panel.Name== "ddngtpanel" || panel.Name== "ddevepanel")
                {
                    dropTime = null;
                    isDrop = false;
                }
                FTrue(isAc, seatType,pickTime,dropTime,pickPoint,travel,dropPoint);
                Filter(isAc, seatType,pickTime,dropTime,pickPoint,travel,dropPoint);
            }
            else
            {
                clearallbutton.ForeColor = SystemColors.Highlight;
                no++;
                if (panel.Name == "acpanel" && nonacpanel.BackColor == colour)
                {
                    nonacpanel.BackColor = Color.White;
                    isAc = "AC";
                    FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
                    isAc = "NON";

                }
                else if (panel.Name == "nonacpanel" && acpanel.BackColor == colour)
                {
                    acpanel.BackColor = Color.White;
                    isAc = "NON";
                    FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
                    isAc = "AC";

                }
                if (panel.Name == "sleeperpanel" && seaterpanel.BackColor == colour)
                {
                    seaterpanel.BackColor = Color.White;
                    seatType = "SL";
                    FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
                    seatType = "ST";
                }
                else if (panel.Name == "seaterpanel" && sleeperpanel.BackColor == colour)
                {
                    sleeperpanel.BackColor = Color.White;
                    seatType = "ST";
                    FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
                    seatType = "SL";
                }
                if (panel.Name=="acpanel") isAc = "AC";
                if(panel.Name=="nonacpanel") isAc = "NON";
                if(panel.Name== "sleeperpanel") seatType = "SL";
                if (panel.Name == "seaterpanel") seatType = "ST";
                if (panel.Name == "putimesrpanel")
                {
                    putimeevepanel.BackColor = Color.White;
                    putimesspanel.BackColor = Color.White;
                    putimengtpanel.BackColor = Color.White;
                    putimeclearbutton.ForeColor = SystemColors.Highlight;
                    pickTime = putimesrlabel.Tag+"";
                    if(isPick) FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
                    isPick = true;
                }
                else if (panel.Name == "putimeevepanel")
                {
                    putimesrpanel.BackColor = Color.White;
                    putimesspanel.BackColor = Color.White;
                    putimengtpanel.BackColor = Color.White;
                    putimeclearbutton.ForeColor = SystemColors.Highlight;
                    pickTime = putimeevelabel.Tag + "";
                    if (isPick) FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
                    isPick = true;
                }
                else if (panel.Name == "putimesspanel")
                {
                    putimesrpanel.BackColor = Color.White;
                    putimeevepanel.BackColor = Color.White;
                    putimengtpanel.BackColor = Color.White;
                    putimeclearbutton.ForeColor = SystemColors.Highlight;
                    pickTime = putimesslabel.Tag + "";
                    if (isPick) FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
                    isPick = true;
                }
                else if (panel.Name == "putimengtpanel")
                {
                    putimesrpanel.BackColor = Color.White;
                    putimeevepanel.BackColor = Color.White;
                    putimesspanel.BackColor = Color.White;
                    putimeclearbutton.ForeColor = SystemColors.Highlight;
                    pickTime = putimengtlabel.Tag + "";
                    if (isPick) FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
                    isPick = true;
                }
                if (panel.Name == "ddsrpanel")
                {
                    ddevepanel.BackColor = Color.White;
                    ddsspanel.BackColor = Color.White;
                    ddngtpanel.BackColor = Color.White;
                    ddtimeclrbutton.ForeColor = SystemColors.Highlight;
                    dropTime = ddsrlabel.Tag + "";
                    if (isDrop) FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
                    isDrop = true;
                }
                else if (panel.Name == "ddevepanel")
                {
                    ddsrpanel.BackColor = Color.White;
                    ddsspanel.BackColor = Color.White;
                    ddngtpanel.BackColor = Color.White;
                    ddtimeclrbutton.ForeColor = SystemColors.Highlight;
                    dropTime = ddevelabel.Tag + "";
                    if (isDrop) FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
                    isDrop = true;
                }
                else if (panel.Name == "ddsspanel")
                {
                    ddevepanel.BackColor = Color.White;
                    ddsrpanel.BackColor = Color.White;
                    ddngtpanel.BackColor = Color.White;
                    ddtimeclrbutton.ForeColor = SystemColors.Highlight;
                    dropTime = ddsslabel.Tag + "";
                    if (isDrop) FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
                    isDrop = true;
                }
                else if (panel.Name == "ddngtpanel")
                {
                    ddevepanel.BackColor = Color.White;
                    ddsspanel.BackColor = Color.White;
                    ddsrpanel.BackColor = Color.White;
                    ddtimeclrbutton.ForeColor = SystemColors.Highlight;
                    dropTime = ddngtlabel.Tag + "";
                    if (isDrop) FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
                    isDrop = true;
                }
                panel.BackColor = colour;                
                Filter(isAc,seatType,pickTime,dropTime,pickPoint,travel,dropPoint);
            }
        }

        private void FTrue(string isAc, string seatType,string picktime,string droptime,string pickpoint,string travel,string droppoint)
        {
            if (isAc==null)
            {
                foreach (var bus in buses)
                {
                    if (bus.SeatType == seatType) bus.Visible = true;
                }
            }
            if(seatType==null)
            {
                foreach (var bus in buses)
                {
                    if (bus.BusType == isAc) bus.Visible = true;
                }
            }
            if(travel==null)
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
            if(pickpoint==null)
            {
                foreach (var bus in buses)
                {
                    if (isAc != null && seatType != null)
                    {
                        if (bus.BusType == isAc && bus.SeatType == seatType) bus.Visible = true;
                        else bus.Visible = false;
                    }
                    else if(isAc != null || seatType != null)
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
            if (picktime==null)
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

        private void Filter(string isAc,string seatType,string picktime,string droptime,string pickpoint,string travel,string droppoint)
        {
            if (clearallbutton.ForeColor == gray)
            {
                Reset();
            }
            if (isAc!=null )
            {
                foreach(var bus in buses)
                {
                    if(bus.BusType==isAc && bus.Visible) bus.Visible = true;
                    else bus.Visible = false;
                }
            }
            if(seatType!=null)
            {
                foreach (var bus in buses)
                {
                    if (bus.SeatType == seatType && bus.Visible) bus.Visible = true;
                    else bus.Visible = false;
                }
            }
            if(picktime!=null)
            {
                string[] time = picktime.Split(' ');
                foreach(var bus in buses)
                {
                    if (Convert.ToDateTime(time[1]) < Convert.ToDateTime(time[0]) && (Convert.ToDateTime(bus.StartTime) >= Convert.ToDateTime(time[0]) || Convert.ToDateTime(bus.StartTime) < Convert.ToDateTime(time[1])) && bus.Visible==true) bus.Visible = true;
                    else if (Convert.ToDateTime(bus.StartTime)>=Convert.ToDateTime(time[0]) && Convert.ToDateTime(bus.StartTime) < Convert.ToDateTime(time[1]) && bus.Visible == true) bus.Visible = true;
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
                    if(c==bus.Boarding.Count) bus.Visible = false;
                }
            }
            if(droppoint!=null)
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
            if(travel!=null)
            {
                foreach (var bus in buses)
                {
                    if (bus.BusName==travel && bus.Visible) bus.Visible = true;
                    else  bus.Visible = false;
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
            foreach(var bus in buses)
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
            if (clearallbutton.ForeColor == highglight)
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
                travel = null;
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
                foreach (CustomCheckbox c in puvaluepanel.Controls)
                {
                    if (c.BackColor == colour) c.Colourchange();
                }
                foreach (CustomCheckbox c in travelvaluepanel.Controls)
                {
                    if (c.BackColor == colour) c.Colourchange();
                }
                foreach (CustomCheckbox c in dpvaluepanel.Controls)
                {
                    if (c.BackColor == colour) c.Colourchange();
                }
                busesfoundlabel.Text=BusesFound()+" Buses Found";
            }
        }

        private void DpclrbuttonClick(object sender, EventArgs e)
        {
            CloseSearch();
            PutimeclearbuttonClick(null, EventArgs.Empty);
            DdtimeclrbuttonClick(null, EventArgs.Empty);
            dpclrbutton.ForeColor = gray;
            foreach (CustomCheckbox c in dpvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    c.Colourchange();
                    no--;
                }
            }
            dropPoint = null;
            FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
            if (no <= 0) clearallbutton.ForeColor = gray;
        }

        private void ClearpickuppointbuttonClick(object sender, EventArgs e)
        {
            CloseSearch();
            PutimeclearbuttonClick(null, EventArgs.Empty);
            DdtimeclrbuttonClick(null, EventArgs.Empty);
            clearpickuppointbutton.ForeColor = gray;
            foreach(CustomCheckbox c in puvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    c.Colourchange();
                    no--;
                }
            }
            pickPoint = null;
            FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
            if (no <= 0) clearallbutton.ForeColor = gray;
        }

        private void TravelclrbuttonClick(object sender, EventArgs e)
        {
            CloseSearch();
            PutimeclearbuttonClick(null, EventArgs.Empty);
            DdtimeclrbuttonClick(null, EventArgs.Empty);
            travelclrbutton.ForeColor = gray;
            foreach (CustomCheckbox c in travelvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    c.Colourchange();
                    no--;
                }
            }
            travel = null;
            FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
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
            isPick = false;
            FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
            no--;
            if (no <= 0) clearallbutton.ForeColor = gray;
        }

        private void OnBusesfoundlabelTextChanged(object sender, EventArgs e)
        {
            adducpanel.Controls.Add(nobuspanel);
            if (busesfoundlabel.Text[0] == '0')
            {
                nobuspanel.Visible = true;
            }
            else
            {
                nobuspanel.Visible = false;
            }
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
            FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
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
                FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
            }
            if (no <= 0) clearallbutton.ForeColor = gray;
            else clearallbutton.ForeColor = highglight;
            Filter(isAc, seatType,pickTime,dropTime,pickPoint,travel,dropPoint);
        }

        private void DropClick(object sender, EventArgs e)
        {
            if (puvaluepanel.Visible == true)
            {
                puvaluepanel.Visible = false;
                pupointpictureBox.Image = Resources.down;
            }
            else Locations(ref isfalse, puvaluepanel, pupointpictureBox,boardingpoints,ref isNot);
        }

        private void FstbuttonClick(object sender, EventArgs e)
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
            endofbuslabel.Dock = DockStyle.Top;
            endofbuslabel.BringToFront();
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
            endofbuslabel.Dock = DockStyle.Top;
            endofbuslabel.BringToFront();
        }

        private void ComboBoxTextChanged(object sender, EventArgs e)
        {
            string[] ss = fromcomboBox.Text.Split(',');
            fromcomboBox.Text = ss[0];
            string[] s = tocomboBox.Text.Split(',');
            tocomboBox.Text = s[0];
            if(fromcomboBox.Text==tocomboBox.Text) warningLabel.Visible = true;
            else warningLabel.Visible = false;
        }

        private int BusesFound()
        {
            int count = 0;
            foreach(var bus in buses)
            {
                if (bus.Visible == true) count++;
            }
            if (count == 0)
            {
                endofbuslabel.Visible = false;
            }
            else
            {
                endofbuslabel.Visible = true;
            }
            return count;            
        }

        private void DprbuttonClick(object sender, EventArgs e)
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
            endofbuslabel.Dock = DockStyle.Top;
            endofbuslabel.BringToFront();
        }

        private void TPictureBoxClick(object sender, EventArgs e)
        {
            List<object> listOfObjects = traveloperatorpoints.Select(s => (object)s).ToList();
            if (travelvaluepanel.Visible == true)
            {
                travelvaluepanel.Visible = false;
                travelpictureBox.Image = Resources.down;
            }
            else Locations(ref isTravel, travelvaluepanel, travelpictureBox,listOfObjects,ref isADD);
        }

        private void DropPointClick(object sender, EventArgs e)
        {
            if (dpvaluepanel.Visible == true)
            {
                dpvaluepanel.Visible = false;
                dppictureBox.Image = Resources.down;
            }
             Locations(ref isDrops, dpvaluepanel, dppictureBox,droppoints,ref isNUll);
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

        private void AddLocations(ref bool b, Panel p, PictureBox pict, List<List<object>> li, ref bool bb)
        {
            if (!b)
            {
                pict.Image = Resources.arrow_up;
                b = true;
                p.Visible = true;
                if (!bb)
                {
                    for (int i = 0; i < li.Count; i++)
                    {
                        CustomCheckbox checkBoxes = new CustomCheckbox();
                        p.Controls.Add(checkBoxes);
                        checkBoxes.Dock = DockStyle.Top;
                        string[] ss = li[i].ToString().Split('&');
                        if (ss.Length == 1)
                        {
                            checkBoxes.SetValuesCheckbox(ss[0]);
                        }
                        else checkBoxes.SetValuesCheckbox(ss[1]);
                        checkBoxes.checks += CheckBoxeschecks;
                    }
                    bb = true;
                }
            }
            else
            {
                pict.Image = Resources.down;
                p.Visible = false;
                b = false;
            }
        }

        private void Locations(ref bool b, Panel p, PictureBox pict, List<object> li, ref bool bb)
        {
            if (!b)
            {
                pict.Image = Resources.arrow_up;
                b = true;
                p.Visible = true;
                if (!bb)
                {
                    for (int i = 0; i < li.Count; i++)
                    {
                        CustomCheckbox checkBoxes = new CustomCheckbox();
                        p.Controls.Add(checkBoxes);
                        checkBoxes.Dock = DockStyle.Top;
                        string[] ss = li[i].ToString().Split('&');
                        if (ss.Length == 1)
                        {
                            checkBoxes.SetValuesCheckbox(ss[0]);
                        }
                        else checkBoxes.SetValuesCheckbox(ss[1]);
                        checkBoxes.checks += CheckBoxeschecks;
                    }
                    bb = true;
                }
            }
            else
            {
                pict.Image = Resources.down;
                p.Visible = false;
                b = false;
            }
        }

        private void CheckBoxeschecks(object sender, EventArgs e)
        {
            CloseSearch();
            pickPoint = null;
            dropPoint = null;
            travel = null;
            FTrue(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
            foreach (CustomCheckbox c in puvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    pickPoint = c.checkBoxs.Text;
                    clearpickuppointbutton.ForeColor = highglight;
                }
                else
                {
                    c.SetCheckedState();
                }

            }
            foreach (CustomCheckbox c in travelvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    travelclrbutton.ForeColor = highglight;
                    travel = c.checkBoxs.Text;
                }
                else
                {
                    c.SetCheckedState();
                }
            }
            foreach (CustomCheckbox c in dpvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    dropPoint = c.checkBoxs.Text;
                    dpclrbutton.ForeColor = highglight;
                }
                else
                {
                    c.SetCheckedState();
                }
            }
            clearallbutton.ForeColor = highglight;
            no++;
            Filter(isAc, seatType, pickTime, dropTime, pickPoint, travel, dropPoint);
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
                DisposeControl(c);
            }
        }
        private void DisposeControl(Control control)
        {
            if (control == null) return;
            foreach (Control child in control.Controls)
            {
                DisposeControl(child);
            }
            control.Dispose();
            control.Text = null;
            control.BackgroundImage?.Dispose(); 
            control.BackgroundImage = null;
            if (control is PictureBox && (control as PictureBox).Image != null) (control as PictureBox).Image.Dispose();
        }
    }
}
