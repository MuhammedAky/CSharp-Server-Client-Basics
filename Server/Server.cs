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
        public static SortedDictionary<int, Client> clients = new SortedDictionary<int, Client>();

        public static void setupServer(int _maxPlayers, int _port)
        {
            MaxPlayers = _maxPlayers;
            Port = _port;

            serverListener = new TcpListener(IPAddress.Any, Port);

            InitializeServerDate();

            Console.WriteLine($"Server installed! : Max Player number: {MaxPlayers}");
        }

        public static void StartServer()
        {
            serverListener.Start();
            Console.WriteLine($"Server started on port: {Port}");

            serverListener.BeginAcceptTcpClient(AcceptClientCallBack, null);
            Console.WriteLine("Waiting for players");
        }

        public static void AcceptClientCallBack(IAsyncResult asyncResult)
        {


            TcpClient socket = serverListener.EndAcceptTcpClient(asyncResult);
            serverListener.BeginAcceptTcpClient(AcceptClientCallBack, null);

            Console.WriteLine("Player joining.");

            for (int i = 1; i <= clients.Count; i++)
            {
                if (clients[i].tcp.socket == null)
                {
                    clients[i].tcp.Connect(socket);
                    return;
                }
            }
            socket.Close();

            Console.WriteLine("Server full");
        }

        public static void InitializeServerDate()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                clients.Add(i, new Client(i));
            }
        }
    }
}
