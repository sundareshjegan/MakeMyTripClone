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
    public partial class CustomCheckBox3 : UserControl
    {
        public CustomCheckBox3()
        {
            InitializeComponent();
        }
        public event EventHandler checks3;
        public void SetValuesCheckbox(string s)
        {
            checkBoxs.Text = s;
        }

        private void CustomCheckboxClick(object sender, EventArgs e)
        {
            foreach (CustomCheckBox3 c in Parent.Controls.OfType<CustomCheckBox3>())
            {
                if (c != this)
                {
                    c.checkBoxs.Checked = false;
                    c.BackColor = Color.White;
                }
            }

            checkBoxs.Checked = true;
            BackColor = SystemColors.GradientInactiveCaption;

            checks3?.Invoke(this, EventArgs.Empty);
        }
        public void SetCheckedState()
        {
            if (checkBoxs.Checked)
            {
                foreach (CustomCheckBox3 c in Parent.Controls.OfType<CustomCheckBox3>())
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
