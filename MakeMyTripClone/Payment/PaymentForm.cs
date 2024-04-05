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
    public partial class PaymentForm : Form
    {
        public PaymentForm()
        {
            InitializeComponent();
        }

        private Panel previousPanel = null;
        private void PaymentGatewayPanelClick(object sender, EventArgs e)
        {
            Panel currentPanel = sender as Panel;
            if (sender is Label label) currentPanel = label.Parent as Panel;
            if (sender is PictureBox pb) currentPanel = pb.Parent as Panel;

            if(previousPanel != null)
            {
                previousPanel.BackColor = Color.Transparent;
                foreach (Control control in previousPanel.Controls)
                {
                    if (control is Panel blueBar)
                        blueBar.Visible = false;
                }
            }
            
            foreach (Control control in currentPanel.Controls)
            {
                if (control is Panel blueBar)
                    blueBar.Visible = true;
            }
            currentPanel.BackColor = Color.White;

            previousPanel = currentPanel;
        }
    }
}
