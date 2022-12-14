using System;
using System.Collections.Generic;
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
            listClient.Add(client);
            server.BeginAcceptTcpClient(AcceptClientCallback, null);
        }

        public void SendToAllClient(Package package)
        {
            MoveData message = (MoveData)package;
            Console.WriteLine($"{message.id} : {message.moveTo} : {message.GetSize()}");
            foreach (TcpClient client in listClient)
            {
                client.Client.SendAsync(package.Serialize(), SocketFlags.None);
            }
        }
    }
}

