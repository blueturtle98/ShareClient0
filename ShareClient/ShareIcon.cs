using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Net;

namespace ShareClient
{
    class ShareIcon
    {

        const int updateInterval = 1000;

        private NetworkInterface net;
        //Disconnect menu consts
        const string connectMenuText = "Connect to server...";

        //Connected menu consts


        private NotifyIcon NotificationIcon;
        private ConnectionInfo currentInfo;

        public void CreateIcon(NetworkInterface n0)
        {
            net = n0;
            NotificationIcon = new NotifyIcon();
            NotificationIcon.Icon = ShareClient.Properties.Resources.IconRed;
            NotificationIcon.Visible = true;

            Thread n = new Thread(Update);
            n.Start();

            return;
        }

        public void UpdateInfo(ConnectionInfo con)
        {
            currentInfo = con;
        }

        private void Update()
        {
            while (true)
            {
                try
                {
                    if (currentInfo.isConnected)
                    {
                        NotificationIcon.Icon = ShareClient.Properties.Resources.IconGreen;

                    }
                    else
                    {
                        NotificationIcon.Icon = ShareClient.Properties.Resources.IconRed;
                        BuildDisconnectedMenu();
                    }

                }catch(Exception e)
                {
                    Debug.WriteLine("Error updating notification icon " + e.Message);
                } 
            }
        }

        public void ShowText(string str, int time)
        {
            NotificationIcon.BalloonTipText = str;
            NotificationIcon.ShowBalloonTip(time);
            return;
        }
        private void BuildDisconnectedMenu()
        {

            ContextMenu newMenu = new ContextMenu();

            //Build connect to server menu
            MenuItem connectItem = new MenuItem();
            connectItem.Text = connectMenuText;
            connectItem.Click += new EventHandler(Connect);

            newMenu.MenuItems.Add(connectItem);
            NotificationIcon.ContextMenu = newMenu;

        }

        private void BuildConnectedMenu()
        {
            ContextMenu newMenu = new ContextMenu();
        }


        //Run when connect to server clicked
        private void Connect(object sender, EventArgs e)
        {
            using (ConnectForm conF = new ConnectForm())
            {
                if(conF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if(conF.userName != "" && conF.serverPort != "" && conF.serverAddr != "")
                    {
                        try
                        {
                            IPAddress.Parse(conF.serverAddr);
                        }catch
                        {
                            ShowText("Invalid server address...", 3);
                            return;
                        }

                        try
                        {
                            Int32.Parse(conF.serverPort);
                        }
                        catch
                        {
                            ShowText("Invalid server port...", 3);
                            return;
                        }
                        
                        net.StartConnect(this, IPAddress.Parse(conF.serverAddr), Int32.Parse(conF.serverPort), conF.userName);
                    }
                    else
                    {
                        NotificationIcon.BalloonTipText = "Missing info";
                        NotificationIcon.ShowBalloonTip(3);
                    }
                }
            }
        }
    }
}
