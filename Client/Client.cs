using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client1
{
    class Client
    {
        public static TcpClient client = new TcpClient();

        public static void Connect()
        {
            client.BeginConnect(ServerSettings.HOST, ServerSettings.PORT,new AsyncCallback(ConnectCallBack) ,null);
            Console.WriteLine("Connecting...");
        }

        public static void ConnectCallBack(IAsyncResult asyncResult)
        {
            client.EndConnect(asyncResult);
            if (client.Connected)
            {
                Console.WriteLine("Connected");
            }
        }
    }
}
