using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using Server.ExtensionMethod;
using Server.NetworkPackage;

namespace Server
{
    public sealed class ServerManager
    {
        static ServerManager instance;
        //private static object lockInstance = new object();
        public static ServerManager Instance
        {
            get
            {
                if (instance == null) instance = new ServerManager();
                return instance;
            }
        }

        IPEndPoint? endPoint;
        TcpListener tcp;
        UdpClient udp;
        List<TcpClient> listTCP;
        List<UdpClient> listUDP;
        NetworkStream? receiveData;
        private byte[] readBuff = new byte[512];

        private ServerManager()
        {
            tcp = new TcpListener(IPAddress.Any, 1995);
            udp = new UdpClient(1995);
            listTCP = new List<TcpClient>();
        }

        public void Start()
        {
            tcp.Server.ReceiveBufferSize = 512;
            tcp.Start();
            tcp.BeginAcceptTcpClient(AcceptClientCallback, null);
        }

        private void AcceptClientCallback(IAsyncResult result)
        {
            TcpClient client = tcp.EndAcceptTcpClient(result);
            Console.WriteLine(listTCP.Count + " : " + client.Client.RemoteEndPoint?.ToString() + " Connected");
            lock (listTCP)
            {
                listTCP.Add(client);
            }
            receiveData = client.GetStream();
            receiveData.BeginRead(readBuff, 0, tcp.Server.ReceiveBufferSize, TCPReceive, null);
            tcp.BeginAcceptTcpClient(AcceptClientCallback, null);
            udp.BeginReceive(UDPReceive, null);
        }

        private void UDPReceive(IAsyncResult ar)
        {
            byte[] udpReceived = udp.EndReceive(ar, ref endPoint);
            List<Package> listPackage = udpReceived.Deserialize();
            listPackage[0].Execution();
            udp.BeginReceive(UDPReceive, null);
        }

        private void TCPReceive(IAsyncResult ar)
        {
            int receivedSize = receiveData.EndRead(ar);
            Console.WriteLine("Received data from " + receiveData.Socket.RemoteEndPoint);
            if (receivedSize <= 0)
            {
                CloseConnection();
                return;
            }
            byte[] data = new byte[receivedSize];
            Buffer.BlockCopy(readBuff, 0, data, 0, receivedSize);
            LoginData pack = new LoginData(data);
            Console.WriteLine(pack.userName);
            receiveData.BeginRead(readBuff, 0, tcp.Server.ReceiveBufferSize, TCPReceive, null);
        }

        private void CloseConnection()
        {
            Console.WriteLine("Closed Connection");
            listTCP = new List<TcpClient>();
        }

        public void UDPSendToAllClient(byte[] package)
        {
            lock (listTCP)
            {
                Console.WriteLine($"Send UDP Package to {listTCP.Count} client");
                foreach (TcpClient client in listTCP)
                {
                    udp.Send(package, package.Length, (IPEndPoint?)client.Client.RemoteEndPoint);
                }
            }
        }

        public void TCPSendToAllClient(Package package)
        {
            lock (listTCP)
            {
                byte[] sendData = package.Serialize();
                Console.WriteLine($"Send TCP Package to {listTCP.Count} client");
                foreach (TcpClient client in listTCP)
                {
                    client.Client.Send(sendData, SocketFlags.None);
                }
            }
        }

        public void TCPSendToAllClient(byte[] package)
        {
            lock (listTCP)
            {
                Console.WriteLine($"Send TCP Package to {listTCP.Count} client");
                foreach (TcpClient client in listTCP)
                {
                    client.Client.Send(package, SocketFlags.None);
                }
            }
        }
    }
}

