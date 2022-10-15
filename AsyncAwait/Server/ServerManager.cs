using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Server.ExtensionMethod;
using Server.NetworkPackage;

namespace Server
{
    public class ServerManager
    {
        TcpListener server;
        TcpClient serverClient;
        List<TcpClient> listClient;
        private static object _listLock = new object();


        NetworkStream receiveData;
        private byte[] readBuff = new byte[512];

        public ServerManager()
        {
            server = new TcpListener(IPAddress.Any, 1995);
            listClient = new List<TcpClient>();
        }
        public void Start()
        {
            server.Server.ReceiveBufferSize = 512;
            server.Start();
            server.BeginAcceptTcpClient(AcceptClientCallback, null);
        }

        private void AcceptClientCallback(IAsyncResult result)
        {
            TcpClient client = server.EndAcceptTcpClient(result);
            Console.WriteLine(listClient.Count + " : " + client.Client.RemoteEndPoint.ToString() + " Connected");
            lock (listClient)
            {
                listClient.Add(client);
            }
            receiveData = client.GetStream();
            receiveData.BeginRead(readBuff, 0, server.Server.ReceiveBufferSize, ReadData, null);
            server.BeginAcceptTcpClient(AcceptClientCallback, null);
        }

        private void ReadData(IAsyncResult ar)
        {
            var receivedSize = receiveData.EndRead(ar);
            Console.WriteLine("Received data from " + receiveData.Socket.RemoteEndPoint);
            if (receivedSize <= 0)
            {
                CloseConnection();
            }
            byte[] data = new byte[receivedSize];
            Buffer.BlockCopy(readBuff, 0, data, 0, receivedSize);
            LoginData pack = new LoginData(data);
            Console.WriteLine(pack.userName);
            receiveData.BeginRead(readBuff, 0, server.Server.ReceiveBufferSize, ReadData, null);
        }

        private void CloseConnection()
        {
            Console.WriteLine("Closed Connection");
        }

        public void SendToAllClient(Package package)
        {
            lock (listClient)
            {
                Console.WriteLine($"Send to {listClient.Count} client");
                foreach (TcpClient client in listClient)
                {
                    client.Client.SendAsync(package.Serialize(), SocketFlags.None);
                }
            }
        }

        public void SendToAllClient(byte[] package)
        {
            lock (listClient)
            {
                Console.WriteLine($"Send to {listClient.Count} client");
                foreach (TcpClient client in listClient)
                {
                    client.Client.SendAsync(package, SocketFlags.None);
                }
            }
        }
    }
}

