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
        }
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

        private Color colour = SystemColors.GradientInactiveCaption;
        private Color gray = Color.DimGray;
        private Color highglight = SystemColors.Highlight;
        private Color white = Color.White;
        private bool isfalse, isTime, isDtime;
        private int no = 0;
        private string selected; 

        private  void Locations(ref bool b,Panel p,PictureBox pict,List<object> li,ref bool bb)
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
                        if(ss.Length==1)
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
            foreach (CustomCheckbox c in puvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    clearpickuppointbutton.ForeColor = highglight;
                }
            }
            foreach (CustomCheckbox c in travelvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    travelclrbutton.ForeColor = highglight;
                }
            }
            foreach (CustomCheckbox c in dpvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    dpclrbutton.ForeColor = highglight;
                }
            }         
            clearallbutton.ForeColor = highglight;
            no++;
        }

        private void Time(ref bool b,Panel p,PictureBox picture)
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
        private Buses bus;
        public static List<RouteDetails> busesList = new List<RouteDetails>();
        public static List<object> droppoints = new List<object>();
        public static List<object> boardingpoints = new List<object>();
        public static List<string> traveloperatorpoints = new List<string>();
        private bool isNUll;
        private bool isTravel;
        private bool isDrop;
        private bool isADD;
        private bool isNot;
        private List<Buses> buses = new List<Buses>();
        private List<RouteDetails> filterlist = new List<RouteDetails>();
       // private List<BusDetails> availableBuses = new List<BusDetails>();
       // private List<BusDetails> filterBuses = new List<BusDetails>();

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

        public void SetData(string boarding , string destination , string date,ComboBox from,ComboBox to,DateTimePicker dateTime)
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
           // departdateTimePicker.MinDate = DateTime.Now;
            boardingpoints = DBManager.GetBoardingPoints(boarding, destination, date);
            droppoints = DBManager.GetDropPoints(boarding, destination, date);
            traveloperatorpoints = DBManager.GetTravel(boarding, destination, date);
            BusButton.BackgroundImage = Resources.busblue;
            AddDatas();
        }

        private void AddDatas()
        {
            srchbutton.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, srchbutton.Width, srchbutton.Height, 30, 30));
            LoginButton.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, LoginButton.Width, LoginButton.Height, 30, 30));
            for (int i = 0; i < busesList.Count; i++)
            {
                bus = new Buses();
                adducpanel.Controls.Add(bus);
                bus.Dock = DockStyle.Top;
                buses.Add(bus);
                bus.Setdata(i, fromcomboBox.Text, tocomboBox.Text);
            }
            busesfoundlabel.Text = busesList.Count + " Buses found";
        }

        private void PanelsClick(object sender, EventArgs e)
        {
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
                

            }
            else
            {
                clearallbutton.ForeColor = SystemColors.Highlight;
                no++;
                if(panel.Name=="acpanel")
                {
                    //arr[0] = "AC";
                    selected = "AC";
                }
                if(panel.Name=="nonacpanel")
                {
                  //  arr[0] = "NON-AC";
                }
                if(panel.Name== "sleeperpanel")
                {
                    //arr[1] = "SL"; 
                }
                if (panel.Name == "seaterpanel")
                {
                    //arr[1] = "ST"; 
                }
                
                //if (panel.Name == "acpanel")
                //{
                //    for (int i = 0; i < busesList.Count; i++)
                //    {
                //        if (busesList[i].BusType == "AC-SL" || busesList[i].BusType == "AC-ST" || busesList[i].BusType == "AC-SL/ST")
                //        {
                //            buses[i].Visible = true;
                //        }
                //        else
                //        {
                //            buses[i].Visible = false;
                //        }
                //    }
                //}
                //if (panel.Name == "nonacpanel")
                //{
                //    for (int i = 0; i < busesList.Count; i++)
                //    {
                //        if (busesList[i].BusType == "AC-SL" || busesList[i].BusType == "AC-ST" || busesList[i].BusType == "AC-SL/ST")
                //        {
                //            buses[i].Visible = false;
                //        }
                //        else
                //        {
                //            buses[i].Visible = true;
                //        }
                //    }
                //}
                if (panel.Name == "acpanel" && nonacpanel.BackColor == colour)
                {
                    nonacpanel.BackColor = Color.White;
                }
                else if (panel.Name == "nonacpanel" && acpanel.BackColor == colour)
                {
                    acpanel.BackColor = Color.White;
                }
                //if(panel.Name== "sleeperpanel")
                //{
                //    for (int i = 0; i < busesList.Count; i++)
                //    {
                //        if (busesList[i].BusType == "AC-SL" || busesList[i].BusType == "NON-AC-SL")
                //        {
                //            buses[i].Visible = true;
                //        }
                //        else
                //        {
                //            buses[i].Visible = false ;
                //        }
                //    }
                //}
                //if (panel.Name == "seaterpanel")
                //{
                //    for (int i = 0; i < busesList.Count; i++)
                //    {
                //        if (busesList[i].BusType == "NON-AC-ST" || busesList[i].BusType == "AC-ST")
                //        {
                //            buses[i].Visible = true;
                //        }
                //        else
                //        {
                //            buses[i].Visible = false;
                //        }
                //    }
                //}
                if (panel.Name == "sleeperpanel" && seaterpanel.BackColor == colour)
                {
                    seaterpanel.BackColor = Color.White;
                }
                else if (panel.Name == "seaterpanel" && sleeperpanel.BackColor == colour)
                {
                    sleeperpanel.BackColor = Color.White;
                }
                if (panel.Name == "putimesrpanel")
                {
                    putimeevepanel.BackColor = Color.White;
                    putimesspanel.BackColor = Color.White;
                    putimengtpanel.BackColor = Color.White;
                    putimeclearbutton.ForeColor = SystemColors.Highlight;
                }
                else if (panel.Name == "putimeevepanel")
                {
                    putimesrpanel.BackColor = Color.White;
                    putimesspanel.BackColor = Color.White;
                    putimengtpanel.BackColor = Color.White;
                    putimeclearbutton.ForeColor = SystemColors.Highlight;
                }
                else if (panel.Name == "putimesspanel")
                {
                    putimesrpanel.BackColor = Color.White;
                    putimeevepanel.BackColor = Color.White;
                    putimengtpanel.BackColor = Color.White;
                    putimeclearbutton.ForeColor = SystemColors.Highlight;
                }
                else if (panel.Name == "putimengtpanel")
                {
                    putimesrpanel.BackColor = Color.White;
                    putimeevepanel.BackColor = Color.White;
                    putimesspanel.BackColor = Color.White;
                    putimeclearbutton.ForeColor = SystemColors.Highlight;
                }
                if (panel.Name == "ddsrpanel")
                {
                    ddevepanel.BackColor = Color.White;
                    ddsspanel.BackColor = Color.White;
                    ddngtpanel.BackColor = Color.White;
                    ddtimeclrbutton.ForeColor = SystemColors.Highlight;
                }
                else if (panel.Name == "ddevepanel")
                {
                    ddsrpanel.BackColor = Color.White;
                    ddsspanel.BackColor = Color.White;
                    ddngtpanel.BackColor = Color.White;
                    ddtimeclrbutton.ForeColor = SystemColors.Highlight;
                }
                else if (panel.Name == "ddsspanel")
                {
                    ddevepanel.BackColor = Color.White;
                    ddsrpanel.BackColor = Color.White;
                    ddngtpanel.BackColor = Color.White;
                    ddtimeclrbutton.ForeColor = SystemColors.Highlight;
                }
                else if (panel.Name == "ddngtpanel")
                {
                    ddevepanel.BackColor = Color.White;
                    ddsspanel.BackColor = Color.White;
                    ddsrpanel.BackColor = Color.White;
                    ddtimeclrbutton.ForeColor = SystemColors.Highlight;
                }
                panel.BackColor = colour;
                Filter(panel);
            }
        }

        private void Filter(Panel panel)
        {
            List<Buses> filbus = new List<Buses>();
            if(panel.Name=="acpanel")
            {
                foreach(var item in buses)
                {
                    if(item.BusType.Contains("AC") && !item.BusType.Contains("NON-AC"))
                    {
                        filbus.Add(item);
                    }
                }
            }
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
                Reset();
                no = 0;
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
                    if (c.BackColor == colour) c.SetCheckedState();
                }
                foreach (CustomCheckbox c in travelvaluepanel.Controls)
                {
                    if (c.BackColor == colour) c.SetCheckedState();
                }
                foreach (CustomCheckbox c in dpvaluepanel.Controls)
                {
                    if (c.BackColor == colour) c.SetCheckedState();
                }
            }
        }
        private void ClearpickuppointbuttonClick(object sender, EventArgs e)
        {
            clearpickuppointbutton.ForeColor = gray;
            foreach(CustomCheckbox c in puvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    c.SetCheckedState();
                    no--;
                }
            }
            if (no <= 0) clearallbutton.ForeColor = gray;
        }

        private void TravelclrbuttonClick(object sender, EventArgs e)
        {
            travelclrbutton.ForeColor = gray;
            foreach (CustomCheckbox c in travelvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    c.SetCheckedState();
                    no--;
                }
            }
            if (no <= 0) clearallbutton.ForeColor = gray;
        }

        private void PutimeclearbuttonClick(object sender, EventArgs e)
        {
            putimesrpanel.BackColor = white;
            putimeevepanel.BackColor = white;
            putimesspanel.BackColor = white;
            putimengtpanel.BackColor = white;
            putimeclearbutton.ForeColor = gray;
            no--;
            if (no <= 0) clearallbutton.ForeColor = gray;
        }
        
        private void DdtimeclrbuttonClick(object sender, EventArgs e)
        {
            ddsrpanel.BackColor = white;
            ddevepanel.BackColor = white;
            ddsspanel.BackColor = white;
            ddngtpanel.BackColor = white;
            ddtimeclrbutton.ForeColor = gray;
            no--;
            if (no <= 0) clearallbutton.ForeColor = gray;
        }

        private void SeatersleepercheckBoxCheckedChanged(object sender, EventArgs e)
        {
            if (seatersleepercheckBox.Checked) no++;
            else no--;
            if (no <= 0) clearallbutton.ForeColor = gray;
            else clearallbutton.ForeColor = highglight;
            //foreach(var datas in buses)
            //{
            //    if(datas.bustype== "AC-ST" || datas.bustype== "NON-AC-ST") datas.Visible = true;
            //    else datas.Visible = false;
            //}
            
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
                bus.Dock = DockStyle.None;
            }
            foreach (var bus in fastest)
            {
                bus.Dock = DockStyle.Top;
                bus.BringToFront();
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
                    bus.Dock = DockStyle.None;
                }
                foreach (var bus in arrival)
                {
                    bus.Dock = DockStyle.Top;
                    bus.BringToFront();
                }
            
        }

        private void DpclrbuttonClick(object sender, EventArgs e)
        {
            dpclrbutton.ForeColor = gray;
            foreach (CustomCheckbox c in dpvaluepanel.Controls)
            {
                if (c.BackColor == colour)
                {
                    c.SetCheckedState();
                    no--;
                }
            }
            if (no <= 0) clearallbutton.ForeColor = gray;
        }

        private void ComboBoxTextChanged(object sender, EventArgs e)
        {
            string[] ss = fromcomboBox.Text.Split(',');
            fromcomboBox.Text = ss[0];
            string[] s = tocomboBox.Text.Split(',');
            tocomboBox.Text = s[0];
            if(fromcomboBox.Text==tocomboBox.Text)
            {
                warningLabel.Visible = true;
            }
            else
            {
                warningLabel.Visible = false;
            }
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
                bus.Dock = DockStyle.None;
            }
            foreach (var bus in departure)
            {
                bus.Dock = DockStyle.Top;
                bus.BringToFront();
            }
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
             Locations(ref isDrop, dpvaluepanel, dppictureBox,droppoints,ref isNUll);
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

    }
}
