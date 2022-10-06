using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;


namespace Client1
{
    class Client
    {
        public static int id;
        public static TcpClient socket = new TcpClient();
        public static NetworkStream stream;
        public static byte[] buffer;
        private static int bufferSize = 4096;

        public static void Connect()
        {
            socket.BeginConnect(ServerSettings.HOST, ServerSettings.PORT,new AsyncCallback(ConnectCallBack) ,null);
            Console.WriteLine("Connecting...");
        }
         
        public static void Disconnect()
        {
            socket.Close();
            stream = null;
            buffer = null;

            if (!socket.Connected)
            {
                Console.WriteLine("Connection Disabled");
            }
        }

        public static void ConnectCallBack(IAsyncResult asyncResult)
        {
            socket.EndConnect(asyncResult);
            if (socket.Connected)
            {
                stream = socket.GetStream();
                buffer = new byte[bufferSize];
                Console.WriteLine("Connected");
                stream.BeginRead(buffer, 0, buffer.Length, ReceiveCallBack, null);
            }
        }

        public static void ReceiveCallBack(IAsyncResult asyncResult)
        {
            try
            {
                int dataLength = stream.EndRead(asyncResult);

                if (dataLength <= 0)
                {
                    return;
                }

                byte[] _data = new byte[dataLength];
                Array.Copy(buffer, _data, dataLength);


                string receiveJson = Encoding.UTF8.GetString(_data);
                Handlers.Handler(receiveJson);

                if (stream! = null)
                {
                    stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReceiveCallBack), null);
                }

            }
            catch (Exception e)
            {
                // Disconnect
                return;
            }
        }

        public static void SendDataFromJson(string _jsonData)
        {
            byte[] _data = Encoding.UTF8.GetBytes(_jsonData);

            try
            {
                stream.BeginWrite(_data, 0, _data.Length, SendCallBack,null);
            } 
            catch(Exception ex)
            {
                // Disconnect
            }
        }

        public static void SendCallBack(IAsyncResult asyncResult)
        {
            stream.EndWrite(asyncResult);
        }
    }
}
