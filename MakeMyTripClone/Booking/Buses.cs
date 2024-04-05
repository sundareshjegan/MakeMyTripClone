﻿using System;
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
            
        }

        public string[] ruppees;
        private Color colour = SystemColors.GradientInactiveCaption, white = Color.White, highlight = SystemColors.Highlight;
        private bool isPhotos;
        private bool isAmenties;
        private Address address;
        public string pickuplocation, droplocation,name,bustype;

        public  void Setdata(int i,string pickup,string drop)
        {
            pickuplocation = pickup;
            droplocation = drop;
            busnamelabel.Text=BookingPageForm.busesList[i].BusName;
            name = busnamelabel.Text;
            busltypeabel.Text = BookingPageForm.busesList[i].BusType;
            bustype = busltypeabel.Text;
            rulabel.Text = BookingPageForm.busesList[i].Price;
            ruppees= rulabel.Text.Split('/');
            DateTime from =Convert.ToDateTime( BookingPageForm.busesList[i].StartDate);
            fromlabel.Text = BookingPageForm.busesList[i].StartTime +" "+ from.Day+" "+MonthName(from.Month);
            DateTime to=Convert.ToDateTime(BookingPageForm.busesList[i].EndDate);
            // durationlabel.Text=BookingPageForm.busesList[i].
            tolabel.Text = BookingPageForm.busesList[i].EndTime + " " + to.Day + " " + MonthName(to.Month);
            noofseatlabel.Text = "";
            if (busltypeabel.Text =="AC-SL" || busltypeabel.Text=="NON-AC-SL")
            {
                lwrlabel.Visible = true;
                uprlabel.Visible = true;
                lbpanel.Visible = true;
                ubpanel.Visible = true;
                seaterpanel.Visible = false;
                Sleeper lbs = new Sleeper();
                lbpanel.Controls.Add(lbs);
                lbs.colours += Lbscolours;
                Sleeper ubs = new Sleeper();
                ubpanel.Controls.Add(ubs);
                ubs.colours += Ubscolours; 
            }
            if (busltypeabel.Text == "AC-SL/ST" || busltypeabel.Text == "NON-AC-SL/ST")
            {
                
                lwrlabel.Visible = true;
                uprlabel.Visible = true;
                lbpanel.Visible = true;
                ubpanel.Visible = true;
                seaterpanel.Visible = false;
                Semiseaters lbs = new Semiseaters();
                lbpanel.Controls.Add(lbs);
                lbs.coloured += Lbscoloured1;
                lbs.notcoloured += Lbsnotcoloured;
                Sleeper ubs = new Sleeper();
                ubpanel.Controls.Add(ubs);
                ubs.colours += Ubscolourss;
            }
            if (busltypeabel.Text == "AC-ST" || busltypeabel.Text == "NON-AC-ST")
            {
                lwrlabel.Visible = false;
                uprlabel.Visible = false;
                lbpanel.Visible = false;
                ubpanel.Visible = false;
                seaterpanel.Visible = true;
                Seaterseats seats = new Seaterseats();
                seaterpanel.Controls.Add(seats);
                seats.seatscolours += Seatsseatscolours;
            }
            foreach(var n in BookingPageForm.droppoints)
            {
                address = new Address();
                string[] ss = n.ToString().Split('&');
                address.AddAddress(ss[0],ss[1], ss[2]);
                droppointpanel.Controls.Add(address);
                address.Dock = DockStyle.Top;
                address.drops += Address_drops;
            }
            foreach(var n in BookingPageForm.boardingpoints)
            {
                address = new Address();
                string[] ss = n.ToString().Split('&');
                address.AddAddress(ss[0], ss[1], ss[2]);
                pickuppointpanel.Controls.Add(address);
                address.Dock = DockStyle.Top;
                address.drops2 += Address_drops2; ;
            }
        }

        private void Address_drops2(object sender, EventArgs e)
        {
            if (prev2 != null)
            {
                prev2.IsClicked = false;
            }
                (sender as Address).IsClicked2 = true;
            prev2 = sender as Address;
            foreach (Address controls in pickuppointpanel.Controls)
            {
                if (controls.BackColor == SystemColors.GradientInactiveCaption)
                {
                    dropvalue = controls;
                    string[] s = address.Getdetails(dropvalue);
                    droppoint = s;
                    // MessageBox.Show($"mm : {s[0]},ss : {s[1]}");
                }
            }
        }

        private Address prev = null,dropvalue=null,prev2=null;
        private string[] boardingpoint, droppoint;
        private void Address_drops(object sender, EventArgs e)
        {
            
            if (prev != null)
            {
                prev.IsClicked = false;
            }
                (sender as Address).IsClicked = true;
            prev = sender as Address;
            foreach(Address controls in droppointpanel.Controls)
            {
                if(controls.BackColor==SystemColors.GradientInactiveCaption)
                {
                    dropvalue = controls;
                    string[] s = address.Getdetails(dropvalue);
                    boardingpoint = s;
                   // MessageBox.Show($"mm : {s[0]},ss : {s[1]}");
                }
            }
            
        }

        private string MonthName(int i)
        {
            if(i==1) return "January";
            if(i==2) return "February";
            if (i == 3) return "March";
            if (i == 4) return "April";
            if(i==5) return "May";
            if(i==6) return "June";
            if(i==7) return "July";
            if(i==8) return "August";
            if(i==9) return "September";
            if(i==10) return "October";
            if(i==11) return "November";
            if(i==12) return "December";
            else
            {
                return "none";
            }
        }
       
        private void Seatsseatscolours(bool e,string s)
        {           
            if (!e)
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) - Convert.ToInt32(rulabel.Text) + "";
                s = s + " , ";
                noofseatlabel.Text=noofseatlabel.Text.Replace(s, "");
            }
            else
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) + Convert.ToInt32(rulabel.Text) + "";
                noofseatlabel.Text += s+" , ";
            }
        }

        private void Lbscolours(bool e,string s)
        {
            if (!e)
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) - Convert.ToInt32(rulabel.Text) + "";
                s = s + " , ";
                noofseatlabel.Text = noofseatlabel.Text.Replace(s, "");
            }
            else
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) + Convert.ToInt32(rulabel.Text) + "";
                noofseatlabel.Text += s + " , ";
            }
        }
        private void Ubscolours(bool e, string s)
        {
            if (!e)
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) - Convert.ToInt32(rulabel.Text) + "";
                s = "u" + s + " , ";
                noofseatlabel.Text = noofseatlabel.Text.Replace(s, "");
            }
            else
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) + Convert.ToInt32(rulabel.Text) + "";
                noofseatlabel.Text += "u" + s + " , ";
            }
        }


        private void Ubscolourss(bool e,string s)
        {
           
            if(!e)
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) - Convert.ToInt32(ruppees[1]) + "";
                s = "u" + s + " , ";
                noofseatlabel.Text = noofseatlabel.Text.Replace(s, "");
            }
            else
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) + Convert.ToInt32(ruppees[1]) + "";
                noofseatlabel.Text += "u" + s + " , ";
            }
        }

        private void Lbsnotcoloured( bool e,string s)
        {
           
            if (!e)
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) - Convert.ToInt32(ruppees[0])+ "";
                s = s + " , ";
                noofseatlabel.Text = noofseatlabel.Text.Replace(s, "");
            }
            else
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) - Convert.ToInt32(ruppees[1]) + "";
                s = s + " , ";
                noofseatlabel.Text = noofseatlabel.Text.Replace(s, "");
            }
        }

        private void Lbscoloured1(bool e,string s)
        {
            if (!e)
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) + Convert.ToInt32(ruppees[0]) + "";
                noofseatlabel.Text += s + " , ";
            }
            else
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) + Convert.ToInt32(ruppees[1]) + "";
                noofseatlabel.Text += s + " , ";
            }
        }

        private void AmentbuttonClick(object sender, EventArgs e)
        {
            if (!isAmenties)
            {
                ptsbutton.BackColor = white;
                amentbutton.BackColor = colour;
                combinationpanel.Visible = false;
                amentiespanel.Visible = true;
                Height = 400;
                isAmenties = true;
                ssbutton.Text = "Select seats";
                ssbutton.BackColor = highlight;
                ssbutton.ForeColor = white;
            }
            else
            {
                ptsbutton.BackColor = white;
                amentiespanel.Visible = false;
                isAmenties = false;
                Height = 200;
            }
        }
        private BookingDetails details = new BookingDetails();
        private void ContinuebutClick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(totalamtlabel.Text) > 0 && boardingpoint != null && droppoint != null)
            {
                details.BusName = busnamelabel.Text;
                details.Bustype = busltypeabel.Text;
                details.Bookedseatnumber = noofseatlabel.Text.Remove(noofseatlabel.Text.Length-3);
                string[] ss = details.Bookedseatnumber.Split(',');
                details.Nooftravellers = ss.Length;
                details.Pickuptime = fromlabel.Text;
                details.Droptime = tolabel.Text;
                details.Pickuplocation = pickuplocation;
                details.Droplolocation = droplocation;
                details.Boardingpoint = boardingpoint;
                details.Droppoint = droppoint;
                //details.dura
                details.Totalamount = Convert.ToInt32(totalamtlabel.Text);
            }
        }


        private void PtsbuttonClick(object sender, EventArgs e)
        {
            if (!isPhotos)
            {
                ptsbutton.BackColor = colour;
                amentbutton.BackColor = white;              
                photospanel.Visible = true;
                combinationpanel.Visible = false;
                isPhotos = true;
                Height = 385;
                ssbutton.Text = "Select seats";
                ssbutton.BackColor = highlight;
                ssbutton.ForeColor = white;
            }
            else
            {
                ptsbutton.BackColor = white;
                photospanel.Visible = false;
                isPhotos = false;
                Height = 200;
            }
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
                photospanel.Visible = false;
                amentiespanel.Visible = false;
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

