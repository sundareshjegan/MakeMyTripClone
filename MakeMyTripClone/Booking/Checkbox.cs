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
       // public delegate void Datas(bool b, int n);
        public event EventHandler<int> checks;
        public void SetValuesCheckbox(string s)
        {
            checkBoxs.Text =s;
        }
        private bool bo;
       
        private void CustomCheckboxClick(object sender, EventArgs e)
        {
            int n = 0;
            CheckBox c = sender as CheckBox;
            if (!bo)
            {
                c.Checked = true;
                bo = true;
                n++;
                
            }
            else
            {
                c.Checked = false;
                bo = false;
                n--;
               
            }
            checks?.Invoke(this, n);
        }
        public void SetCheckedState(bool isChecked)
        {
            checkBoxs.Checked = isChecked;
        }
    }
}
