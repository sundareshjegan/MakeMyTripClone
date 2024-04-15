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
using System.Text.RegularExpressions;

namespace MakeMyTripClone
{
    public partial class Buses : UserControl
    {
        public Buses()
        {
            InitializeComponent();
            
        }

        private Color colour = SystemColors.GradientInactiveCaption, white = Color.White, highlight = SystemColors.Highlight;
        private bool isPhotos, isAmenties;
        private string pickuplocation, droplocation,bustype,duration,starttime,endtime,acnonac,seat,busname;
        private List<object> boarding = new List<object>();
        private List<object> destination = new List<object>();
        private int rid, bid;
        private Address address,prev = null, dropvalue = null, prev2 = null;
        private string[] boardingpoint, droppoint, ruppees;
        private BookingDetails details = new BookingDetails();

        public string BusType
        {
            get { return acnonac; }
        }
        public string SeatType
        {
            get { return seat; }
        }
        public string BusName
        {
            get { return busname; }
        }

        public string Duration
        {
            get { return duration; }
        }
        
        public string StartTime
        {
            get { return starttime; }
        }

        public List<string> Boarding
        {
            get { return  boarding.Select(obj => obj.ToString()).ToList(); ; }
        }

        public List<string> Droping
        {
            get { return destination.Select(obj => obj.ToString()).ToList(); ; }
        }

        public string EndTime
        {
            get { return endtime; }
        }

        public  void Setdata(int i,string pickup,string drop,string date)
        {
            rid = BookingPageForm.busesList[i].RouteId;
            bid = BookingPageForm.busesList[i].BusId;
            pickuplocation = pickup;
            droplocation = drop;
            busnamelabel.Text=BookingPageForm.busesList[i].BusName;
            busname = busnamelabel.Text;
            busltypeabel.Text = BookingPageForm.busesList[i].BusType;
            string[] s = busltypeabel.Text.Split('-');
            acnonac = s[0];
            seat = s[s.Length - 1];
            bustype = busltypeabel.Text;
            rulabel.Text = BookingPageForm.busesList[i].Price;
            ruppees= rulabel.Text.Split('/');
            DateTime from =Convert.ToDateTime( BookingPageForm.busesList[i].StartDate);
            fromlabel.Text = BookingPageForm.busesList[i].StartTime +" "+ from.Day+" "+MonthName(from.Month);
            starttime = BookingPageForm.busesList[i].StartTime;
            durationlabel.Text = BookingPageForm.busesList[i].Duration;
            duration = durationlabel.Text;
            DateTime to = Convert.ToDateTime(BookingPageForm.busesList[i].EndDate);
            tolabel.Text = BookingPageForm.busesList[i].EndTime + " " + to.Day + " " + MonthName(to.Month);
            endtime = BookingPageForm.busesList[i].EndTime;
            noofseatlabel.Text = "";
            if (busltypeabel.Text =="AC-SL" || busltypeabel.Text=="NON-AC-SL")
            {
                lwrlabel.Visible = true;
                uprlabel.Visible = true;
                lbpanel.Visible = true;
                ubpanel.Visible = true;
                seaterpanel.Visible = false;
                Sleeper lbs = new Sleeper();
                foreach(PictureBox seatPb in lbs.Controls)
                {
                    string ss = seatPb.Name.Remove(0, 10);
                    if (DBManager.IsSeatBooked(rid, ss))
                    {
                        seatPb.Enabled = false;
                        seatPb.BackgroundImage = Properties.Resources.bookedSleeper;
                    }
                }
                lbpanel.Controls.Add(lbs);
                lbs.colours += Lbscolours;
                Sleeper ubs = new Sleeper();
                ubpanel.Controls.Add(ubs);
                foreach (PictureBox seatPb in ubs.Controls)
                {
                    string sss = "u"+seatPb.Name.Remove(0, 10);
                    if (DBManager.IsSeatBooked(rid,sss))
                    {
                        seatPb.Enabled = false;
                        seatPb.BackgroundImage = Properties.Resources.bookedSleeper;
                    }
                }
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
                foreach (PictureBox seatPb in lbs.Controls)
                {
                    string ss = seatPb.Name.Remove(0, 10);
                    if (DBManager.IsSeatBooked(rid, ss))
                    {
                        seatPb.Enabled = false;
                        if(ss=="1" || ss=="6" || ss=="11" || ss=="16" || ss=="21") seatPb.BackgroundImage = Properties.Resources.bookedSleeper;
                        else seatPb.BackgroundImage = Properties.Resources.bookedSeater;
                    }
                }
                lbs.coloured += Lbscoloured1;
                lbs.notcoloured += Lbsnotcoloured;
                Sleeper ubs = new Sleeper();
                ubpanel.Controls.Add(ubs);
                foreach (PictureBox seatPb in ubs.Controls)
                {
                    string sss = "u" + seatPb.Name.Remove(0, 10);
                    if (DBManager.IsSeatBooked(rid, sss))
                    {
                        seatPb.Enabled = false;
                        seatPb.BackgroundImage = Properties.Resources.bookedSleeper;
                    }
                }
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
                foreach (PictureBox seatPb in seats.Controls)
                {
                    string ss = seatPb.Name.Remove(0, 10);
                    if (DBManager.IsSeatBooked(rid, ss))
                    {
                        seatPb.Enabled = false;
                        seatPb.BackgroundImage = Properties.Resources.bookedSeater;
                    }
                }
                seats.seatscolours += Seatsseatscolours;
            }
            boarding = DBManager.GetBoarding(pickup, drop, date, rid);
            destination = DBManager.GetDrop(pickup, drop, date, rid);
            foreach(var n in destination)
            {
                address = new Address();
                string[] ss = n.ToString().Split('&');
                address.AddAddress(ss[0],ss[1], ss[2]);
                droppointpanel.Controls.Add(address);
                address.Dock = DockStyle.Top;
                address.drops += Addressdrops;
            }
            foreach(var n in boarding)
            {
                address = new Address();
                string[] ss = n.ToString().Split('&');
                address.AddAddress(ss[0], ss[1], ss[2]);
                pickuppointpanel.Controls.Add(address);
                address.Dock = DockStyle.Top;
                address.drops2 += Addressdropss; 
            }
        }

        private void Addressdropss(object sender, EventArgs e)
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
                    boardingpoint = s;
                }
            }
        }

        private void Addressdrops(object sender, EventArgs e)
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
                    droppoint = s;
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

        public void Srch()
        {
            ssbutton.Text = "Select seats";
            ssbutton.BackColor = highlight;
            ssbutton.ForeColor = white;
            Height = 200;
            toppanel.BackColor = white;
        }

        private void Lbscolours(bool e,string s)
        {
            if (!e)
            {
               totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) - Convert.ToInt32(rulabel.Text) + ""; //
               s = s + " , ";
               if(s==noofseatlabel.Text)
               {

               }
               noofseatlabel.Text = Regex.Replace(noofseatlabel.Text, @"\b" + s + @"\b", "");
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
                noofseatlabel.Text = Regex.Replace(noofseatlabel.Text, @"\b" + s + @"\b", "");
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
                photospanel.Visible = false;
                ptsbutton.BackColor = white;
                amentbutton.BackColor = colour;
                combinationpanel.Visible = false;
                amentiespanel.Visible = true;
                Height = 400;
                isAmenties = true;
                ssbutton.Text = "Select seats";
                ssbutton.BackColor = highlight;
                ssbutton.ForeColor = white;
                isPhotos = false;
            }
            else
            {
                amentbutton.BackColor = white;
                ptsbutton.BackColor = white;
                amentiespanel.Visible = false;
                isAmenties = false;
                Height = 200;
            }
        }

        private void ContinuebutClick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(totalamtlabel.Text) > 0 && boardingpoint != null && droppoint != null)
            {
                details.RootId = rid;
                details.BusId = bid;
                details.BusName = busnamelabel.Text;
                details.Bustype = busltypeabel.Text;
                details.Bookedseatnumber = noofseatlabel.Text.Remove(noofseatlabel.Text.Length-3);
                string[] ss = details.Bookedseatnumber.Split(',');
                details.Nooftravellers = ss.Length;
                details.Pickuptime = fromlabel.Text;
                details.Droptime = tolabel.Text;
                details.Pickuplocation = pickuplocation;
                details.Droplocation = droplocation;
                details.Boardingpoint = boardingpoint;
                details.Droppoint = droppoint;
                details.Durations = durationlabel.Text;
                details.Totalamount = Convert.ToInt32(totalamtlabel.Text);
                details.seatAmount = rulabel.Text;
                CompleteBookingForm completeBooking = new CompleteBookingForm();
                completeBooking.SetData(details);
                completeBooking.ShowDialog();
            }
        }


        private void PtsbuttonClick(object sender, EventArgs e)
        {
            if (!isPhotos)
            {
                amentiespanel.Visible = false;
                ptsbutton.BackColor = colour;
                amentbutton.BackColor = white;              
                photospanel.Visible = true;
                combinationpanel.Visible = false;
                isPhotos = true;
                Height = 385;
                ssbutton.Text = "Select seats";
                ssbutton.BackColor = highlight;
                ssbutton.ForeColor = white;
                isAmenties = false;
            }
            else
            {
                ptsbutton.BackColor = white;
                amentbutton.BackColor = white;
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
                amentbutton.BackColor = white;
                ptsbutton.BackColor = white;
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

