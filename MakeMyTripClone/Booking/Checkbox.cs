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
        private bool bo;
       
        private void CustomCheckboxClick(object sender, EventArgs e)
        {
            try
            {
                CheckBox c = sender as CheckBox;
                if (!bo)
                {
                    c.Checked = true;
                    bo = true;
                    BackColor = SystemColors.GradientInactiveCaption;
                }
                else
                {
                    c.Checked = false;
                    bo = false;
                    BackColor = Color.White;
                }
            }
            catch
            {
                if (!bo)
                {
                    checkBoxs.Checked = true;
                    bo = true;
                    BackColor = SystemColors.GradientInactiveCaption;
                }
                else
                {
                    checkBoxs.Checked = false;
                    bo = false;
                    BackColor = Color.White;
                }
            }
            checks?.Invoke(this, EventArgs.Empty);
        }
        public void SetCheckedState()
        {
            BackColor = Color.White;
            checkBoxs.Checked = false;
        }
        


    }
}
