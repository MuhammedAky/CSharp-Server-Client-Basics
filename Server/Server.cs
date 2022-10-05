using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server1
{
    class Server
    {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static TcpListener serverListener;
        // public static SortedDictionary<int, Client> clients = new SortedDictionary<int, Client>();

        public static void setupServer(int _maxPlayers, int _port)
        {
            MaxPlayers = _maxPlayers;
            Port = _port;

            serverListener = new TcpListener(IPAddress.Any, Port);

            Console.WriteLine($"Server kuruldu! : Maxsimum oyuncu sayısı: {MaxPlayers}");
        }

        public static void StartServer()
        {
            serverListener.Start();
            Console.WriteLine($"Server başlatıldı! : {Port}'unda dinleniyor...");

            serverListener.BeginAcceptTcpClient(AcceptClientCallBack, null);
            Console.WriteLine("Oyuncular Bekleniyor");
        }

        public static void AcceptClientCallBack(IAsyncResult asyncResult)
        {


            TcpClient socket = serverListener.EndAcceptTcpClient(asyncResult);

            Console.WriteLine($"Bİr oyuncu içeri girdi. {socket.Client.RemoteEndPoint}");

            serverListener.BeginAcceptTcpClient(AcceptClientCallBack, null);

        }
    }
}
