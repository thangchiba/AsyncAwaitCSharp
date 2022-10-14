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
        List<TcpClient> listClient;
        private static object _listLock = new object();

        public ServerManager()
        {
            server = new TcpListener(IPAddress.Any, 1995);
            listClient = new List<TcpClient>();
        }
        public void Start()
        {
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
            server.BeginAcceptTcpClient(AcceptClientCallback, null);
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

