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
    public partial class ConnectForm : Form
    {

        public string serverAddr;
        public string serverPort;
        public string userName;


        public ConnectForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.serverAddr = serverBox.Text;
            this.serverPort = portBox.Text;
            this.userName = nameBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ConnectForm_Load(object sender, EventArgs e)
        {

        }

        private void portBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
