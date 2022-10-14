
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
    public class ClientTCP2
    {
        IPEndPoint ServerIP;
        public TcpClient client;
        NetworkStream receiveData;
        private byte[] readBuff = new byte[512];

        public void Connect()
        {
            ServerIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1995);
            client = new TcpClient(AddressFamily.InterNetwork);
            try
            {
                client.Connect(ServerIP);
                receiveData = client.GetStream();
                Array.Resize(ref readBuff, client.ReceiveBufferSize);
                receiveData.BeginRead(readBuff, 0, client.ReceiveBufferSize, ReceiveData, null);
                Console.WriteLine("Kết nối sv thành công");
            }
            catch (Exception e)
            {
                Console.WriteLine("Khong the ket noi sv");
            }
        }

        private void ReceiveData(IAsyncResult ar)
        {
            try
            {
                int receivedSize = receiveData.EndRead(ar);
                if (client == null) return;
                if (receivedSize <= 0)
                {
                    CloseConnection();
                    return;
                }
                client.NoDelay = false;
                byte[] receivedData = new byte[receivedSize];
                Buffer.BlockCopy(readBuff, 0, receivedData, 0, receivedSize);
                MoveData receivedPackage = receivedData.Deserialize<MoveData>();
                AddMessage(receivedPackage);
                receiveData.BeginRead(readBuff, 0, client.ReceiveBufferSize, ReceiveData, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot Received Data");
                CloseConnection();
            }
        }

        private void CloseConnection()
        {
            Console.WriteLine("Closed Connection");
        }

        public void Send(Package message)
        {
            client.Client.Send(message.Serialize());
            //AddMessage(message);
        }
        public void AddMessage(MoveData message)
        {
            //Console.WriteLine(message.id + " : " + message.moveTo);
        }
    }
}
