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
        public event EventHandler drops,drops2;
        private bool isClicked;
        public bool IsClicked {get { return isClicked; } set
            {
                isClicked = value;
                if(value)
                {
                    BackColor= SystemColors.GradientInactiveCaption;
                }
                else
                {
                    BackColor = Color.White;
                }
            } }
        private bool isClicked2;
        public bool IsClicked2
        {
            get { return isClicked2; }
            set
            {
                isClicked2 = value;
                if (value)
                {
                    BackColor = SystemColors.GradientInactiveCaption;
                }
                else
                {
                    BackColor = Color.White;
                }
            }
        }
        public void AddAddress(string s1,string s2,string s3)
        {
            datetimelabel.Text = s1;
            stopnamelabel.Text = s2;
            addresslabel.Text = s3;
        }

        private void AddressClick(object sender, EventArgs e)
        {
            drops?.Invoke(this, EventArgs.Empty);
            drops2 ?.Invoke(this, EventArgs.Empty);
        }
        public string[] Getdetails(Address address)
        {
            string[] s = new string[3];
            s[0] = address.datetimelabel.Text;
            s[1] = address.stopnamelabel.Text;
            s[2] = address.addresslabel.Text;
            return s;
        }
    }
}
