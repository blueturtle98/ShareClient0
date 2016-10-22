using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ShareClient
{
    class NetworkInterface
    {

        private TcpClient tcpC;
        private ShareIcon icon;
        private BinaryFormatter binF;


        public void StartConnect(ShareIcon icon, IPAddress svrAddr, int svrPort, string username)
        {
            icon.ShowText("Connecting to: " + svrAddr.ToString() + ":" + svrPort.ToString(), 1);

            try
            {
                tcpC = new TcpClient();
                tcpC.Connect(svrAddr, svrPort);
                Thread runThread = new Thread(WaitForMessage);
                runThread.Start();
                return;
                
            }
            catch
            {
                icon.ShowText("Failed to connect", 3);
                return;
            }

        }

        private void WaitForMessage()
        {
            while (true)
            {
                Stream stm = tcpC.GetStream();
                MemoryStream ms = new MemoryStream();

                //Get the size of the message (serialized)
                byte[] headerByte = new byte[8];
                stm.Read(headerByte, 0, 8);
                //Write the data to memorystream
                ms.Write(headerByte, 0, 8);
                //deserialized the size int
                int msgSize = (int)binF.Deserialize(ms);
                Debug.WriteLine("Receiving message of {0} bytes", msgSize.ToString());

                ms = new MemoryStream();
            }
        }

    }
}
