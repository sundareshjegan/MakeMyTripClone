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
        public event EventHandler<bool> Coloured;
        private int count = 0;
        
        private void PictureBoxClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.BorderStyle == BorderStyle.FixedSingle)
            {
                pictureBox.BackgroundImage = Resources.SLEEPER;
                pictureBox.BorderStyle = BorderStyle.None;
                count -= 1;
            }
            else
            {
                pictureBox.BackgroundImage = Resources.Bluesleeper;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                count += 1;
            }
            if(count>0)
            {
                Coloured?.Invoke(null,true);
            }
        }
    }
}
