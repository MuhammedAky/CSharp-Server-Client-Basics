using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;


namespace Server1
{
    class Client
    {
        private static int bufferSize = 4096;
        public int id;
        public TCP tcp;

        public Client(int _id)
        {
            id = _id;
            tcp = new TCP(id);
        }

        public void Disconnect()
        {
            tcp.Disconnect();
        }

        public class TCP{

            public TcpClient socket;
            public readonly int id;
            public NetworkStream stream;
            public byte[] buffer;


            public TCP(int _id)
            {
                id = _id;
            }

            public void Disconnect()
            {
                socket.Close();
                stream = null;
                socket = null;
                buffer =null;

                Console.WriteLine($"{id}. Client Disconnected!");
            }

            public void Connect(TcpClient _socket)
            {
                socket = _socket;
                socket.ReceiveBufferSize = bufferSize;
                socket.SendBufferSize = bufferSize;

                stream = socket.GetStream();
                buffer = new byte[bufferSize];
                stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReceiveCallBack) ,null);
                Console.WriteLine($"Connected: {id}");
            }

            public void ReceiveCallBack(IAsyncResult asyncResult)
            {
                try
                {
                    int dataLength = stream.EndRead(asyncResult);
                    
                    if (dataLength <= 0)
                    {
                        Disconnect();
                        return;
                    }

                    byte[] _data = new byte[dataLength];
                    Array.Copy(buffer, _data, dataLength);


                    string receiveString = Encoding.UTF8.GetString(_data);
                    Console.WriteLine($"Text came from {id}.Player, text: {receiveString}");

                    stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReceiveCallBack), null);
                } catch (Exception e)
                {
                    // Disconnect
                    Disconnect();
                    return;
                }
            }

            public void SendDataFromJson(string _jsonData)
            {
                byte[] _data = Encoding.UTF8.GetBytes(_jsonData);

                try
                {
                    stream.BeginWrite(_data, 0, _data.Length, SendCallBack, null);
                }
                catch (Exception ex)
                {
                    // Disconnect
                }
            }

            public void SendCallBack(IAsyncResult asyncResult)
            {
                stream.EndWrite(asyncResult);
            }
        }
    }
}