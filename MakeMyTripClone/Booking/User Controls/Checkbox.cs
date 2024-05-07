using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeMyTripClone.Booking
{
    public partial class CustomCheckbox : UserControl
    {
        public CustomCheckbox()
        {
            InitializeComponent();
        }
        public event EventHandler checks;
        public void SetValuesCheckbox(string s)
        {
            checkBoxs.Text =s;
        }
       
        private void CustomCheckboxClick(object sender, EventArgs e)
        {
            foreach (CustomCheckbox c in Parent.Controls.OfType<CustomCheckbox>())
            {
                if (c != this)
                {
                    c.checkBoxs.Checked = false;
                    c.BackColor = Color.White;
                }
            }

            checkBoxs.Checked = true;
            BackColor = SystemColors.GradientInactiveCaption;

            checks?.Invoke(this, EventArgs.Empty);
        }
        public void SetCheckedState()
        {
            if (checkBoxs.Checked)
            {
                foreach (CustomCheckbox c in Parent.Controls.OfType<CustomCheckbox>())
                {
                    if (c != this)
                    {
                        c.checkBoxs.Checked = false;
                        c.BackColor = Color.White;
                    }
                }
            }
            else
            {
                BackColor = Color.White;
            }
        }
        public void Colourchange()
        {
            checkBoxs.Checked = false;
            BackColor = Color.White;
        }


    }
}
