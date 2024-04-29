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
    public partial class Semiseaters : UserControl
    {
        public Semiseaters()
        {
            InitializeComponent();
        }
        public delegate void DatasHandler(bool b, string s);
        public event DatasHandler coloured;
        public new void Dispose()
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if ((Controls[i] as PictureBox).BackgroundImage != null) (Controls[i] as PictureBox).BackgroundImage.Dispose();
            }
        }
        private void PictureBoxClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.BackgroundImage != null) pictureBox.BackgroundImage.Dispose();
            string s = pictureBox.Name.Remove(0, 10);
            if (pictureBox.BorderStyle == BorderStyle.FixedSingle)
            {
                if (s == "1" || s == "6" || s == "11" || s == "16" || s == "21")
                {
                    pictureBox.BackgroundImage = Resources.SLEEPER;
                    pictureBox.BorderStyle = BorderStyle.None;
                }
                else
                {
                    pictureBox.BackgroundImage = Resources.Seater;
                    pictureBox.BorderStyle = BorderStyle.None;
                }
                coloured?.Invoke( false, s);
            }
            else
            {
                if (s == "1" || s == "6" || s == "11" || s == "16" || s == "21")
                {
                    pictureBox.BackgroundImage = Resources.Bluesleeper;
                    pictureBox.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    pictureBox.BackgroundImage = Resources.Blueseater;
                    pictureBox.BorderStyle = BorderStyle.FixedSingle;
                }
                coloured?.Invoke( true,s);
                ListAdd(s);
            }
        }

        private void ListAdd(string s)
        {
            if (s != "1" && s != "6" && s != "11" && s != "16" && s != "21")
            {
                if (s == "2" || s == "4" || s == "7" || s == "9" || s == "12" || s == "14" || s == "17" || s == "19" || s == "22" || s == "24")
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
