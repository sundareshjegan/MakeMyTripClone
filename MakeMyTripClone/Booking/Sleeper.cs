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
    public partial class Sleeper : UserControl
    {
      
        public Sleeper()
        {
            InitializeComponent();
        }
        public delegate void DatasHandler(bool b, string s);
        public event DatasHandler colours;
        private void PictureBoxClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.BorderStyle == BorderStyle.FixedSingle)
            {
                pictureBox.BackgroundImage = Resources.SLEEPER;
                pictureBox.BorderStyle = BorderStyle.None;
                colours?.Invoke(false, pictureBox.Name.Remove(0, 10));
            }
            else
            {
                pictureBox.BackgroundImage = Resources.Bluesleeper;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                colours?.Invoke(true, pictureBox.Name.Remove(0, 10));
            }
           
        }

       

    }
}
