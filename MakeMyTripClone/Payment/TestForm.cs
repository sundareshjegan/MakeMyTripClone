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
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
            // Enable scripting
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.IsWebBrowserContextMenuEnabled = true;

            // Enable ActiveX controls
            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.WebBrowserShortcutsEnabled = true;
            webBrowser1.Url = new Uri("https://www.youtube.com/watch?v=My_S68DAAPg");
        }
    }
}
