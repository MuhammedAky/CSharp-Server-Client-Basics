using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server1{
    class Program{
        
        static void Main(string[] args){
            Server.setupServer(500, 8585);
            Server.StartServer();

            Console.ReadKey();
        }
    }


}