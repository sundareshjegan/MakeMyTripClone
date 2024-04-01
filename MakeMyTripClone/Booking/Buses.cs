using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MakeMyTripClone
{
    public partial class Buses : UserControl
    {
        public Buses()
        {
            InitializeComponent();
            sleeper.Coloured += Sleeper_Coloured;
        }

        private void Sleeper_Coloured(object sender, bool e)
        {
            continuebut.BackColor = highlight;
        }

        public Sleeper sleeper = new Sleeper();
        public string Busname
        {
            get => Busname;
            set
            {
                busnamelabel.Text = value;
            }
        }
        public  void Setdata()
        {

        }
        [DllImport("user32.dll")]
        private static extern bool PostMessage(
           IntPtr hWnd, 
           Int32 msg, 
           Int32 wParam, 
           Int32 lParam 
           );
        const Int32 WM_LBUTTONDOWN = 0x0201;
        private Color colour = SystemColors.GradientInactiveCaption,white= Color.White,highlight= SystemColors.Highlight;

        private void AmentbuttonClick(object sender, EventArgs e)
        {
            ptsbutton.BackColor = white;
            amentbutton.BackColor = colour;
            reviewbutton.BackColor = white;
        }

        private void ReviewbuttonClick(object sender, EventArgs e)
        {
            ptsbutton.BackColor = white;
            amentbutton.BackColor = white;
            reviewbutton.BackColor = colour;
        }

        private void comboBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pickupcomboBox.DroppedDown)
            {
                if (!pickupcomboBox.Bounds.Contains(PointToClient(Cursor.Position)))
                {
                   
                }
                else
                {
                    pickupcomboBox.DroppedDown = false;
                }
            }
        }
        public static  Sleeper lbs;
        
        private void Buses_Load(object sender, EventArgs e)
        {  
            if (busltypeabel.Text=="sleeper")
            {
                lwrlabel.Visible = true;
                uprlabel.Visible = true;
                lbpanel.Visible = true;
                ubpanel.Visible = true;
                seaterpanel.Visible = false;
                lbs = new Sleeper();
                lbpanel.Controls.Add(lbs);
                Sleeper ubs = new Sleeper();
                ubpanel.Controls.Add(ubs);
            }
            if(busltypeabel.Text=="semiseater")
            {
                lwrlabel.Visible = true;
                uprlabel.Visible = true;
                lbpanel.Visible = true;
                ubpanel.Visible = true;
                seaterpanel.Visible = false;
                Semiseaters lbs = new Semiseaters();
                lbpanel.Controls.Add(lbs);
                Sleeper ubs = new Sleeper();
                ubpanel.Controls.Add(ubs);

            }
            if(busltypeabel.Text== "seater")
            {
                lwrlabel.Visible = false;
                uprlabel.Visible = false;
                lbpanel.Visible = false;
                ubpanel.Visible = false;
                seaterpanel.Visible = true;
                Seaterseats seats = new Seaterseats();
                seaterpanel.Controls.Add(seats);
            }
        }

     

        private void PtsbuttonClick(object sender, EventArgs e)
        {
            ptsbutton.BackColor = colour;
            amentbutton.BackColor = white;
            reviewbutton.BackColor = white;
        }

        private void SelectClick(object sender, EventArgs e)
        {
            if(ssbutton.Text== "Select seats")
            {
                ssbutton.Text = "Hide seats";
                ssbutton.BackColor =white;
                ssbutton.ForeColor = highlight;
                Height = 1050;
                combinationpanel.Visible = true;
                toppanel.BackColor =colour ;
       
            }
            else
            {
                ssbutton.Text = "Select seats";
                ssbutton.BackColor = highlight;
                ssbutton.ForeColor = white;
                combinationpanel.Visible = false;
                Height = 200;
                toppanel.BackColor = white;
            }
        }
       
        }

    }

