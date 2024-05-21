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
using System.IO;

namespace MakeMyTripClone
{
    public partial class Sleeper : UserControl
    {

        public Sleeper()
        {
            InitializeComponent();
        }
        public delegate void DatasHandler(bool b, string s);
        public event DatasHandler Colours;
        //public new void Dispose()
        //{
        //    for (int i = 0; i < Controls.Count; i++)
        //    {
        //        if ((Controls[i] as PictureBox).BackgroundImage != null) (Controls[i] as PictureBox).BackgroundImage.Dispose();
        //    }
        //}
        private void PictureBoxClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
           // if (pictureBox.BackgroundImage != null) pictureBox.BackgroundImage.Dispose();
            if (pictureBox.BorderStyle == BorderStyle.FixedSingle)
            {
                pictureBox.BackgroundImage = Resources.SLEEPER;
                pictureBox.BorderStyle = BorderStyle.None;
                Colours?.Invoke(false, pictureBox.Name.Remove(0, 10));
            }
            else
            {
                pictureBox.BackgroundImage = Resources.Bluesleeper;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                string s = pictureBox.Name.Remove(0, 10);
                Colours?.Invoke(true, s);
                if (s != "1" && s != "4" && s != "7" && s != "10" && s != "13" && s != "16")
                {
                    if (s == "2" || s == "5" || s == "8" || s == "11" || s == "14" || s == "17")
                    {
                        int n = int.Parse(s) + 1;
                        string p = "pictureBox" + n;
                        PictureBox picture = this.Controls.Find(p, true).FirstOrDefault() as PictureBox;
                        if (picture != null)
                        {
                            if (picture.BackColor == SystemColors.ControlLightLight)
                            {
                                Buses.isFemaleseats.Add(true);
                            }
                            else
                            {
                                Buses.isFemaleseats.Add(false);
                            }
                        }

                    }
                    else
                    {
                        int n = int.Parse(s) - 1;
                        string p = "pictureBox" + n;
                        PictureBox picture = this.Controls.Find(p, true).FirstOrDefault() as PictureBox;
                        if (picture != null)
                        {
                            if (picture.BackColor == SystemColors.ControlLightLight)
                            {
                                Buses.isFemaleseats.Add(true);
                            }
                            else
                            {
                                Buses.isFemaleseats.Add(false);
                            }
                        }
                    }
                }
                else
                {
                    Buses.isFemaleseats.Add(false);
                }
            }
        }

    }
}
