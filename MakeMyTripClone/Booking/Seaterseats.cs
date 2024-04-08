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
            //List<SeatDeatils> seats = DBManager.GetSeatDetails(Tag);
            for(int i = 0;i< seats.Count; i++)
            {

            }
        }
        public delegate void DatasHandler(bool b, string s);
        public event DatasHandler seatscolours;
        private void PictureBoxClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.BorderStyle == BorderStyle.FixedSingle)
            {
                pictureBox.BackgroundImage = Resources.Seater;
                pictureBox.BorderStyle = BorderStyle.None;
                seatscolours?.Invoke(false,pictureBox.Name.Remove(0,10));
            }
            else
            {
                pictureBox.BackgroundImage = Resources.Blueseater;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                seatscolours?.Invoke(true, pictureBox.Name.Remove(0, 10));
            }
        }
    }
}
