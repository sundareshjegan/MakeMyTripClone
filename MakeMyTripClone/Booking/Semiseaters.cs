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
        //public event EventHandler<bool> coloured,notcoloured; 
        public event DatasHandler coloured,notcoloured;
        private void PictureBoxSleeperClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.BorderStyle == BorderStyle.FixedSingle)
            {
                pictureBox.BackgroundImage = Resources.SLEEPER;
                pictureBox.BorderStyle = BorderStyle.None;
                notcoloured?.Invoke(true, pictureBox.Name.Remove(0, 10));
            }
            else
            {
                pictureBox.BackgroundImage = Resources.Bluesleeper;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                coloured?.Invoke(true, pictureBox.Name.Remove(0, 10));
            }
           
        }

        private void PictureBoxSeaterClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.BorderStyle == BorderStyle.FixedSingle)
            {
                pictureBox.BackgroundImage = Resources.Seater;
                pictureBox.BorderStyle = BorderStyle.None;
                notcoloured?.Invoke( false, pictureBox.Name.Remove(0, 10));
            }
            else
            {
                pictureBox.BackgroundImage = Resources.Blueseater;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                coloured?.Invoke( false, pictureBox.Name.Remove(0, 10));
            }
            
        }
       
    }
}
