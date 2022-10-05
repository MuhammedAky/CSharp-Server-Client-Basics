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
        public int id;
        public TCP tcp;

    public Client(int _id)
    {
        id = _id;
        tcp = new TCP(id);
    }

        public class TCP{

            public TcpClient socket;
            public readonly int id;


            public TCP(int _id)
            {
                id = _id;
            }

            public void Connect(TcpClient _socket)
            {
                socket = _socket;
                Console.WriteLine($"Connected: {id}");
            }
        }
    }
}