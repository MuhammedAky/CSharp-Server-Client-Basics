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
        public static TcpClient socket = new TcpClient();

        public static void Connect()
        {
            socket.BeginConnect(ServerSettings.HOST, ServerSettings.PORT,new AsyncCallback(ConnectCallBack) ,null);
            Console.WriteLine("Connecting...");
        }

        public static void ConnectCallBack(IAsyncResult asyncResult)
        {
            socket.EndConnect(asyncResult);
            if (socket.Connected)
            {
                Console.WriteLine("Connected");
            }
        }
    }
}
