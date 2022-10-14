
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Server.ExtensionMethod;
using Server.NetworkPackage;

namespace Server
{
    public class ClientTCP
    {
        IPEndPoint IP;
        Socket client;
        public void Connect()
        {
            IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1995);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                client.Connect(IP);
                Thread receiveThread = new Thread(Receive);
                receiveThread.IsBackground = true;
                receiveThread.Start();
                Console.WriteLine("Kết nối sv thành công");
            }
            catch
            {
                Console.WriteLine("khong the ket noi sv");
            }
        }
        public void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024];
                    client.Receive(data);
                    MoveData message = data.Deserialize<MoveData>();
                    AddMessage(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Loi nhan du lieu");
                Console.WriteLine(e.Message);
            }

        }


        public void Send(MoveData message)
        {
            if (message.id != String.Empty)
                client.Send(message.Serialize());
            AddMessage(message);
        }
        public void AddMessage(MoveData message)
        {
            Console.WriteLine(message.id + " : " + message.moveTo);
        }
    }
}
