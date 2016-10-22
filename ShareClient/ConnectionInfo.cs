using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ShareClient
{
    class ConnectionInfo
    {
        public bool isConnected = false;
        public IPEndPoint serverAddr = null;

        public string clientName = null;
        public int clientId = 999;

        public List<ConnectedClient> clientList = null;
    }
}
