using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeMyTripClone.HomePage
{
    public partial class HomePageForm : Form
    {
        public HomePageForm()
        {
            InitializeComponent();
            DBManager.GetConnection();
        }

        #region DLL to Create rounded Regions
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );
        #endregion

        private void navBar1_Load(object sender, EventArgs e)
        {

        }
    }
}
