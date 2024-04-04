using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeMyTripClone
{
    public partial class Address : UserControl
    {
        public Address()
        {
            InitializeComponent();
        }
        public static List<Address> addresses = new List<Address>();
        public void AddAddress(string s1,string s2,string s3)
        {
            datetimelabel.Text = s1;
            stopnamelabel.Text = s2;
            addresslabel.Text = s3;
        }

        private void AddressClick(object sender, EventArgs e)
        {
            addresses.Add(sender as Address);
            BackColor = SystemColors.GradientInactiveCaption;
          
        }
    }
}
