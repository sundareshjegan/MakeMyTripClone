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
    public partial class LoaderForm : Form
    {
        public LoaderForm()
        {
            InitializeComponent();

            //Timer timer = new Timer();
            //timer.Interval = 2000;
            //timer.Tick += OnLoaderTimerTicked;
            //timer.Start();

        }

        private void OnLoaderTimerTicked(object sender, EventArgs e)
        {
           // OnLoaderOpened?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnLoaderOpened;
    }
}
