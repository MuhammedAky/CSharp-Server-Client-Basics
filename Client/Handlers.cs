using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Client1
{
    class Handlers
    {
        public enum ServerHandler
        {
            Hello = 1
        }

        public enum ClientHandler
        {
            Hello = 1
        }
        public class Packet
        {
            public int id;
            public int type;
        }

        public class HelloPacket : Packet
        {
            public string HelloMessage;
            public bool Connected;
        }

        public static void Handler(string _Json)
        {
            JToken data = JToken.Parse(_Json);

            if ((int)data["type"] == (int)ServerHandler.Hello)
            {
                HelloPacket packet = JsonConvert.DeserializeObject<HelloPacket>(_Json);

                Console.WriteLine(packet.HelloMessage);   

                if (!packet.Connected)
                {
                    Client.Disconnect();
                }
            }
        }
    }
}
