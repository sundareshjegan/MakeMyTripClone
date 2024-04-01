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
        [DllImport("user32.dll")]
        public static extern bool PostMessage(
            IntPtr hWnd, 
            Int32 msg,
            Int32 wParam, 
            Int32 lParam 
            );
        private Color colour = SystemColors.GradientInactiveCaption;
        private Color gray = Color.DimGray;
        private Color highglight = SystemColors.Highlight;
        private Color white = Color.White;
        const Int32 WM_LBUTTONDOWN = 0x0201;
        private bool isfalse,isTime,isTravel,isDrop,isDtime;
        private Buses bus = new Buses()
        {
        };
        private bool ispickup;
        private bool isTravelclr;
        private bool isDp;
        private int no = 0;

        private  void Locations(ref bool b,ComboBox comboBox,Panel p,PictureBox pict)
        {
            if (!b)
            {
                pict.Image = Resources.arrow_up;
                b = true;
                Int32 x = comboBox.Width - 10;
                Int32 y = comboBox.Height / 2;
                Int32 lParam = x + y * 0x00010000;
                PostMessage(comboBox.Handle, WM_LBUTTONDOWN, 1, lParam);
                p.Visible = true;
            }
            else
            {
                pict.Image = Resources.down;
                p.Visible = false;
                b = false;
            }
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
        private static List<RouteDetails> busesList = new List<RouteDetails>();

        private void Form1_Load(object sender, EventArgs e)
        {
            pupointcomboBox.Items.Add("a");
            pupointcomboBox.Items.Add("b");
            travelcomboBox.Items.Add("a");
            travelcomboBox.Items.Add("b");
            dpcomboBox.Items.Add("Chengalpattu");
            dpcomboBox.Items.Add("SRM universitry");
            adducpanel.Controls.Add(bus);
            bus.Dock = DockStyle.Top;
            bus.Setdata();
            Buses b = new Buses();
            adducpanel.Controls.Add(b);
            b.Dock = DockStyle.Top;
           
        }
      
        public void SetData(string boarding , string destination , string date)
        {
            busesList = DBManager.GetBuses(boarding, destination, date);
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
                if (panel.Name == "acpanel" && nonacpanel.BackColor == colour)
                {
                    nonacpanel.BackColor = Color.White;
                }
                else if (panel.Name == "nonacpanel" && acpanel.BackColor == colour)
                {
                    acpanel.BackColor = Color.White;
                }
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
            }
        }

        private void LabelsClick(object sender, EventArgs e)
        {
            Label label = sender as Label;
            PanelsClick(label.Parent, EventArgs.Empty);
        }

        private void PictureboxesClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            PanelsClick(pictureBox.Parent, EventArgs.Empty);
        }

        private void PupointcomboBoxTextChanged(object sender, EventArgs e)
        {
            clearpickuppointbutton.ForeColor = SystemColors.Highlight;
            clearallbutton.ForeColor = highglight;
            if (!ispickup) no++;
        }

        private void ClearallbuttonClick(object sender, EventArgs e)
        {
            if (clearallbutton.ForeColor == highglight)
            {
                no = 0;
                pupointcomboBox.Text = null;
                travelcomboBox.Text = null;
                dpcomboBox.Text = null;
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
            }
        }
        private void ClearpickuppointbuttonClick(object sender, EventArgs e)
        {
            ispickup = true;
            pupointcomboBox.Text = null;
            clearpickuppointbutton.ForeColor = gray;
            no--;
            if (no <= 0) clearallbutton.ForeColor = gray;
            if (ispickup) ispickup = false;
        }

        private void TravelclrbuttonClick(object sender, EventArgs e)
        {
            isTravelclr = true;
            travelcomboBox.Text = null;
            travelclrbutton.ForeColor = gray;
            no--;
            if (no <= 0) clearallbutton.ForeColor = gray;
            if (isTravelclr) isTravelclr = false;
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

        private void DpclrbuttonClick(object sender, EventArgs e)
        {
            isDp = true;
            dpcomboBox.Text = null;
            dpclrbutton.ForeColor = gray;
            no--;
            if (no <= 0) clearallbutton.ForeColor = gray;
            if (isDp) isDp = false;
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

        private void TravelcomboBoxTextChanged(object sender, EventArgs e)
        {
            travelclrbutton.ForeColor = highglight;
            clearallbutton.ForeColor = highglight;
            if (!isTravelclr) no++;
        }

        private void DpcomboBoxTextChanged(object sender, EventArgs e)
        {
            dpclrbutton.ForeColor = highglight;
            clearallbutton.ForeColor = highglight;
            if (!isDp) no++;
        }

        private void SeatersleepercheckBoxCheckedChanged(object sender, EventArgs e)
        {
            if (seatersleepercheckBox.Checked) no++;
            else no--;
            if (no <= 0) clearallbutton.ForeColor = gray;
            else clearallbutton.ForeColor = highglight;
        }

        private void DropClick(object sender, EventArgs e)
        {
            if (puvaluepanel.Visible == true)
            {
                puvaluepanel.Visible = false;
                pupointpictureBox.Image = Resources.down;
            }
            else Locations(ref isfalse, pupointcomboBox, puvaluepanel, pupointpictureBox);
        }

        private void RlvbuttonClick(object sender, EventArgs e)
        {
            rlvbutton.BackColor = colour;
            fstbutton.BackColor = white;
            chpbutton.BackColor = white;
            rtbutton.BackColor = white;
            arbutton.BackColor = white;
            dprbutton.BackColor = white;
        }

        private void FstbuttonClick(object sender, EventArgs e)
        {
            rlvbutton.BackColor = white;
            fstbutton.BackColor = colour;
            chpbutton.BackColor = white;
            rtbutton.BackColor = white;
            arbutton.BackColor = white;
            dprbutton.BackColor = white;
        }

        private void ChpbuttonClick(object sender, EventArgs e)
        {
            rlvbutton.BackColor = white;
            fstbutton.BackColor = white;
            chpbutton.BackColor = colour;
            rtbutton.BackColor = white;
            arbutton.BackColor = white;
            dprbutton.BackColor = white;
        }

        private void RtbuttonClick(object sender, EventArgs e)
        {
            rlvbutton.BackColor = white;
            fstbutton.BackColor = white;
            chpbutton.BackColor = white;
            rtbutton.BackColor = colour;
            arbutton.BackColor = white;
            dprbutton.BackColor = white;
        }

        private void ArbuttonClick(object sender, EventArgs e)
        {
            rlvbutton.BackColor = white;
            fstbutton.BackColor = white;
            chpbutton.BackColor = white;
            rtbutton.BackColor = white;
            arbutton.BackColor = colour;
            dprbutton.BackColor = white;
        }

        private void DprbuttonClick(object sender, EventArgs e)
        {
            rlvbutton.BackColor = white;
            fstbutton.BackColor = white;
            chpbutton.BackColor = white;
            rtbutton.BackColor = white;
            arbutton.BackColor = white;
            dprbutton.BackColor = colour;
        }

        private void TPictureBoxClick(object sender, EventArgs e)
        {
            if (travelvaluepanel.Visible == true)
            {
                travelvaluepanel.Visible = false;
                travelpictureBox.Image = Resources.down;
            }
            else Locations(ref isTravel, travelcomboBox, travelvaluepanel, travelpictureBox);
        }
        private void DropPointClick(object sender, EventArgs e)
        {
            if (dpvaluepanel.Visible == true)
            {
                dpvaluepanel.Visible = false;
                dppictureBox.Image = Resources.down;
            }
            else Locations(ref isDrop, dpcomboBox, dpvaluepanel, dppictureBox);
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
