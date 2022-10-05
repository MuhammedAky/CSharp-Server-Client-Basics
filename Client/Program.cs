using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client1
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerSettings.ServerSet("127.0.0.1", 8585);

            Client.Connect();

            Console.ReadKey();
        }
    }
}

