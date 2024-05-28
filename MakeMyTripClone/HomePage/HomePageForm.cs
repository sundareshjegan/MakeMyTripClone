using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MakeMyTripClone.HomePage
{
    public partial class HomePageForm : Form
    {
        public HomePageForm()
        {
            InitializeComponent();
            DBManager.GetConnection();
            CreateCurves();
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
        
        private void CreateCurves()
        {
            offersPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, offersPanel.Width, offersPanel.Height, 30, 30));
            productOfferPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, productOfferPanel.Width, productOfferPanel.Height, 30, 30));
        }
    }
}
