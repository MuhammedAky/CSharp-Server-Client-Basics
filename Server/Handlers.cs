using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Server1
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

        public static HelloPacket CreateHelloPacket(int _id, bool _connected,int _type, string _HelloMessage)
        {
            HelloPacket packet = new HelloPacket();
            packet.id = _id;
            packet.type = _type;
            packet.HelloMessage = _HelloMessage;
            packet.Connected = _connected;

            return packet;
        }
    }
}
