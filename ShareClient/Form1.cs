using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareClient
{
    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            ShareIcon n = new ShareIcon();
            n.CreateIcon(new NetworkInterface());

            n.UpdateInfo(new ConnectionInfo());
            
        }
    }
}
