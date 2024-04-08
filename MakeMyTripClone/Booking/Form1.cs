using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeMyTripClone
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string key = busId + "_" + rid;
            label1.Tag = key;
        }

        private int busId = 23, rid = 3;

        private void label1_Click(object sender, EventArgs e)
        {
            if(label1.Tag is string id)
            {

            }
        }
    }
}
