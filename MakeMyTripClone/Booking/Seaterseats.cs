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

        private void PictureBoxClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.BorderStyle == BorderStyle.FixedSingle)
            {
                pictureBox.BackgroundImage = Resources.Seater;
                pictureBox.BorderStyle = BorderStyle.None;
            }
            else
            {
                pictureBox.BackgroundImage = Resources.Blueseater;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
            }
        }
    }
}
