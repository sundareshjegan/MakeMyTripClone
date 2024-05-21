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

namespace MakeMyTripClone
{
    public partial class Seaterseats : UserControl
    {
        public Seaterseats()
        {
            InitializeComponent();
        }
        public delegate void DatasHandler(bool b, string s);
        public event DatasHandler Seatscolours;
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
            string s = pictureBox.Name.Remove(0, 10);
           // if (pictureBox.BackgroundImage != null) pictureBox.BackgroundImage.Dispose();
            if (pictureBox.BorderStyle == BorderStyle.FixedSingle)
            {
                pictureBox.BackgroundImage = Resources.Seater;
                pictureBox.BorderStyle = BorderStyle.None;
                Seatscolours?.Invoke(false, s);
            }
            else
            {
                pictureBox.BackgroundImage = Resources.Blueseater;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                Seatscolours?.Invoke(true, s);
                if (s == "1" || s == "5" || s == "9" || s == "13" || s == "17" || s == "21" || s == "25" || s == "29" || s == "33" || s == "37" || s == "3" || s == "7" || s == "11" || s == "15" || s == "19" || s == "23" || s == "27" || s == "31" || s == "35" || s == "39")
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

        }
    }
}
