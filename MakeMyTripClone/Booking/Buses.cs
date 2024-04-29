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
        private string pickuplocation, droplocation, bustype, duration, starttime, endtime, acnonac, seat, busname;
        private List<object> boarding = new List<object>();
        private List<object> destination = new List<object>();
        private int rid, bid;
        private Address address, prev = null, dropvalue = null, prev2 = null;
        private string[] boardingpoint, droppoint, ruppees;
        private BookingDetails details = new BookingDetails();
        public static List<bool> isFemaleseats = new List<bool>();
        private List<string> seatsbooked = new List<string>();

        private static Buses previousClickedBus;

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
            get { return boarding.Select(obj => obj.ToString()).ToList(); }
        }

        public List<string> Droping
        {
            get { return destination.Select(obj => obj.ToString()).ToList(); }
        }

        public string EndTime
        {
            get { return endtime; }
        }

        public void Setdata(int i, string pickup, string drop, string date)
        {
            rid = BookingPageForm.busesList[i].RouteId;
            bid = BookingPageForm.busesList[i].BusId;
            pickuplocation = pickup;
            droplocation = drop;
            busnamelabel.Text = BookingPageForm.busesList[i].BusName;
            busname = busnamelabel.Text;
            busltypeabel.Text = BookingPageForm.busesList[i].BusType;
            string[] s = busltypeabel.Text.Split('-');
            acnonac = s[0];
            seat = s[s.Length - 1];
            bustype = busltypeabel.Text;
            rulabel.Text = BookingPageForm.busesList[i].Price;
            ruppees = rulabel.Text.Split('/');
            DateTime from = Convert.ToDateTime(BookingPageForm.busesList[i].StartDate);
            fromlabel.Text = BookingPageForm.busesList[i].StartTime + " " + from.Day + " " + MonthName(from.Month);
            starttime = BookingPageForm.busesList[i].StartTime;
            durationlabel.Text = BookingPageForm.busesList[i].Duration;
            duration = durationlabel.Text;
            DateTime to = Convert.ToDateTime(BookingPageForm.busesList[i].EndDate);
            tolabel.Text = BookingPageForm.busesList[i].EndTime + " " + to.Day + " " + MonthName(to.Month);
            endtime = BookingPageForm.busesList[i].EndTime;
            noofseatlabel.Text = "";
            boarding = DBManager.GetBoarding(pickup, drop, date, rid);
            destination = DBManager.GetDrop(pickup, drop, date, rid);
            foreach (var n in destination)
            {
                address = new Address();
                string[] ss = n.ToString().Split('&');
                address.AddAddress(ss[0], ss[1], ss[2]);
                droppointpanel.Controls.Add(address);
                address.Dock = DockStyle.Top;
                address.drops += Addressdrops;
            }
            foreach (var n in boarding)
            {
                address = new Address();
                string[] ss = n.ToString().Split('&');
                address.AddAddress(ss[0], ss[1], ss[2]);
                pickuppointpanel.Controls.Add(address);
                address.Dock = DockStyle.Top;
                address.drops2 += Addressdropss;
            }
        }

        private void AddSeatTypeUserControls()
        {
            if (busltypeabel.Text == "AC-SL" || busltypeabel.Text == "NON-AC-SL")
            {
                lwrlabel.Visible = true;
                uprlabel.Visible = true;
                lbpanel.Visible = true;
                ubpanel.Visible = true;
                seaterpanel.Visible = false;
                Sleeper lbs = new Sleeper();
                foreach (PictureBox seatPb in lbs.Controls)
                {
                    string ss = seatPb.Name.Remove(0, 10);
                    if (DBManager.IsSeatBooked(rid, ss) && !DBManager.IsSeatBookedByFemale(rid, ss))
                    {
                        seatPb.Enabled = false;
                        seatPb.BackgroundImage = Properties.Resources.bookedSleeper;
                    }
                    else if (DBManager.IsSeatBookedByFemale(rid, ss))
                    {
                        seatPb.Enabled = false;
                        seatPb.BackgroundImage = Properties.Resources.femaleSleeper;
                        seatPb.BackColor = SystemColors.ControlLightLight;
                    }
                }
                lbpanel.Controls.Add(lbs);
                lbs.colours += Lbscolours;
                Sleeper ubs = new Sleeper();
                ubpanel.Controls.Add(ubs);
                foreach (PictureBox seatPb in ubs.Controls)
                {
                    string sss = "u" + seatPb.Name.Remove(0, 10);
                    if (DBManager.IsSeatBooked(rid, sss) && !DBManager.IsSeatBookedByFemale(rid, sss))
                    {
                        seatPb.Enabled = false;
                        seatPb.BackgroundImage = Properties.Resources.bookedSleeper;
                    }
                    else if (DBManager.IsSeatBookedByFemale(rid, sss))
                    {
                        seatPb.Enabled = false;
                        seatPb.BackgroundImage = Properties.Resources.femaleSleeper;
                        seatPb.BackColor = SystemColors.ControlLightLight;
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
                    if (DBManager.IsSeatBooked(rid, ss) && !DBManager.IsSeatBookedByFemale(rid, ss))
                    {
                        seatPb.Enabled = false;
                        if (ss == "1" || ss == "6" || ss == "11" || ss == "16" || ss == "21") seatPb.BackgroundImage = Properties.Resources.bookedSleeper;
                        else seatPb.BackgroundImage = Properties.Resources.bookedSeater;
                    }
                    else if (DBManager.IsSeatBookedByFemale(rid, ss))
                    {
                        seatPb.Enabled = false;
                        if (ss == "1" || ss == "6" || ss == "11" || ss == "16" || ss == "21")
                        {
                            seatPb.BackgroundImage = Properties.Resources.femaleSleeper;
                            seatPb.BackColor = SystemColors.ControlLightLight;
                        }
                        else
                        {
                            seatPb.BackgroundImage = Properties.Resources.femaleSeater;
                            seatPb.BackColor = SystemColors.ControlLightLight;
                        }
                    }
                }
                lbs.coloured += Coloured;
                Sleeper ubs = new Sleeper();
                ubpanel.Controls.Add(ubs);
                foreach (PictureBox seatPb in ubs.Controls)
                {
                    string sss = "u" + seatPb.Name.Remove(0, 10);
                    if (DBManager.IsSeatBooked(rid, sss) && !DBManager.IsSeatBookedByFemale(rid, sss))
                    {
                        seatPb.Enabled = false;
                        seatPb.BackgroundImage = Properties.Resources.bookedSleeper;
                    }
                    else if (DBManager.IsSeatBookedByFemale(rid, sss))
                    {
                        seatPb.Enabled = false;
                        seatPb.BackgroundImage = Properties.Resources.femaleSleeper;
                        seatPb.BackColor = SystemColors.ControlLightLight;
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
                    if (DBManager.IsSeatBooked(rid, ss) && !DBManager.IsSeatBookedByFemale(rid, ss))
                    {
                        seatPb.Enabled = false;
                        seatPb.BackgroundImage = Properties.Resources.bookedSeater;
                    }
                    else if (DBManager.IsSeatBookedByFemale(rid, ss))
                    {
                        seatPb.Enabled = false;
                        seatPb.BackgroundImage = Properties.Resources.femaleSeater;
                        seatPb.BackColor = SystemColors.ControlLightLight;
                    }
                }
                seats.seatscolours += Seatsseatscolours;
            }
        }
        private void IsRemove(int i)
        {
            isFemaleseats.RemoveAt(i);
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
            foreach (Address controls in droppointpanel.Controls)
            {
                if (controls.BackColor == SystemColors.GradientInactiveCaption)
                {
                    dropvalue = controls;
                    string[] s = address.Getdetails(dropvalue);
                    droppoint = s;
                }
            }

        }

        private string MonthName(int i)
        {
            if (i == 1) return "January";
            if (i == 2) return "February";
            if (i == 3) return "March";
            if (i == 4) return "April";
            if (i == 5) return "May";
            if (i == 6) return "June";
            if (i == 7) return "July";
            if (i == 8) return "August";
            if (i == 9) return "September";
            if (i == 10) return "October";
            if (i == 11) return "November";
            if (i == 12) return "December";
            else
            {
                return "none";
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

        private void Seatsseatscolours(bool e, string s)
        {
            if (!e)
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) - Convert.ToInt32(rulabel.Text) + "";
                IsRemove(seatsbooked.IndexOf(s));
                seatsbooked.Remove(s);
            }
            else
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) + Convert.ToInt32(rulabel.Text) + "";
                seatsbooked.Add(s);
            }
            noofseatlabel.Text = String.Join(" , ", seatsbooked);
        }

        private void Lbscolours(bool e, string s)
        {
            if (!e)
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) - Convert.ToInt32(rulabel.Text) + "";
                IsRemove(seatsbooked.IndexOf(s));
                seatsbooked.Remove(s);
            }

            else
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) + Convert.ToInt32(rulabel.Text) + "";
                seatsbooked.Add(s);
            }
            noofseatlabel.Text = String.Join(" , ", seatsbooked);
        }

        private void Ubscolours(bool e, string s)
        {
            s = "u" + s;
            if (!e)
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) - Convert.ToInt32(rulabel.Text) + "";
                IsRemove(seatsbooked.IndexOf(s));
                seatsbooked.Remove(s);
            }

            else
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) + Convert.ToInt32(rulabel.Text) + "";
                seatsbooked.Add(s);
            }
            noofseatlabel.Text = String.Join(" , ", seatsbooked);
        }

        private void Ubscolourss(bool e, string s)
        {
            s = "u" + s;
            if (!e)
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) - Convert.ToInt32(ruppees[1]) + "";
                IsRemove(seatsbooked.IndexOf(s));
                seatsbooked.Remove(s);
            }
            else
            {
                totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) + Convert.ToInt32(ruppees[1]) + "";
                seatsbooked.Add(s);
            }
            noofseatlabel.Text = String.Join(" , ", seatsbooked);
        }

        private void Coloured(bool e, string s)
        {
            if (!e)
            {
                if (s == "1" || s == "6" || s == "11" || s == "16" || s == "21")
                {
                    totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) + Convert.ToInt32(ruppees[1]) + "";
                }
                else
                {
                    totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) + Convert.ToInt32(ruppees[0]) + "";
                }
                IsRemove(seatsbooked.IndexOf(s));
                seatsbooked.Remove(s);
            }
            else
            {
                if (s == "1" || s == "6" || s == "11" || s == "16" || s == "21")
                {
                    totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) + Convert.ToInt32(ruppees[1]) + "";
                }
                else
                {
                    totalamtlabel.Text = Convert.ToInt32(totalamtlabel.Text) + Convert.ToInt32(ruppees[0]) + "";
                }
                seatsbooked.Add(s);
            }
            noofseatlabel.Text = String.Join(" , ", seatsbooked);
        }

        private void ContinuebutClick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(totalamtlabel.Text) > 0 && boardingpoint != null && droppoint != null)
            {
                details.RootId = rid;
                details.BusId = bid;
                details.BusName = busnamelabel.Text;
                details.Bustype = busltypeabel.Text;
                details.Bookedseatnumber = noofseatlabel.Text;
                details.Nooftravellers = seatsbooked.Count;
                details.Pickuptime = fromlabel.Text;
                details.Droptime = tolabel.Text;
                details.Pickuplocation = pickuplocation;
                details.Droplocation = droplocation;
                details.Boardingpoint = boardingpoint;
                details.Droppoint = droppoint;
                details.Durations = durationlabel.Text;
                details.Totalamount = Convert.ToInt32(totalamtlabel.Text);
                details.seatAmount = rulabel.Text;
                details.FemaleSeatList = isFemaleseats;
                CompleteBookingForm completeBooking = new CompleteBookingForm();
                completeBooking.SetData(details);
                completeBooking.ShowDialog();
            }
            else
            {
                Warninglabelcontinue.Text = "Kindly Select Seats, Boarding and Drop Points";
            }
        }

        private void AmentbuttonClick(object sender, EventArgs e)
        {
            if (!isAmenties)
            {
                photospanel.Visible = false;
                ptsbutton.BackColor = white;
                amentbutton.BackColor = colour;
                amentiespanel.Visible = true;
                amentiespanel.BringToFront();
                combinationpanel.Visible = true;
                combinationpanel.BringToFront();
                Height = 1250;
                isAmenties = true;
                ssbutton.Text = "Hide seats";
                ssbutton.BackColor = white;
                ssbutton.ForeColor = highlight;
                isPhotos = false;
                toppanel.BackColor = colour;
            }
            else
            {
                amentbutton.BackColor = white;
                ptsbutton.BackColor = white;
                amentiespanel.Visible = false;
                isAmenties = false;
                Height = 1050;
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
                photospanel.BringToFront();
                combinationpanel.Visible = true;
                combinationpanel.BringToFront();
                isPhotos = true;
                Height = 1250;
                ssbutton.Text = "Hide seats";
                ssbutton.BackColor = white;
                ssbutton.ForeColor = highlight;
                isAmenties = false;
                toppanel.BackColor = colour;
            }
            else
            {
                ptsbutton.BackColor = white;
                amentbutton.BackColor = white;
                photospanel.Visible = false;
                isPhotos = false;
                Height = 1050;
            }
        }

        private void SelectClick(object sender, EventArgs e)
        {
            if (ssbutton.Text == "Select seats")
            {   
                AddSeatTypeUserControls();
                ssbutton.Text = "Hide seats";
                ssbutton.BackColor = white;
                ssbutton.ForeColor = highlight;
                Height = 1250;
                combinationpanel.Visible = true;
                toppanel.BackColor = colour;
                photospanel.Visible = false;
                amentiespanel.Visible = false;
                amentbutton.BackColor = white;
                ptsbutton.BackColor = white;
                if (previousClickedBus != null && previousClickedBus != this && previousClickedBus.Height == 1250)
                {
                    previousClickedBus.SelectClick(sender, e);
                    previousClickedBus.lbpanel.Controls.Clear();
                    previousClickedBus.ubpanel.Controls.Clear();
                    previousClickedBus.seaterpanel.Controls.Clear();
                    previousClickedBus.totalamtlabel.Text = "0";
                    previousClickedBus.noofseatlabel.Text = "";
                    isFemaleseats.Clear();
                    previousClickedBus.AddSeatTypeUserControls();
                }   
            }
            else
            {
                //if (seats != null) seats.Dispose();
                //if (ubs != null) ubs.Dispose();
                //if (lbs != null) lbs.Dispose();
                //if (semiubs != null) semiubs.Dispose();
                //if (semi != null) semi.Dispose();
                ssbutton.Text = "Select seats";
                ssbutton.BackColor = highlight;
                ssbutton.ForeColor = white;
                combinationpanel.Visible = false;
                Height = 200;
                toppanel.BackColor = white;
            }
            previousClickedBus = this;
        }
    }
}

