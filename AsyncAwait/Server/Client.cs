
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Server.ExtensionMethod;
using Server.NetworkPackage;

namespace Server
{
    public class Client
    {
        IPEndPoint ServerAddress;
        IPEndPoint ClientAddress;
        public TcpClient tcp;
        public UdpClient udp;
        IPEndPoint? endPoint;
        NetworkStream receiveData;
        private byte[] readBuff = new byte[512];

        public Client(int port)
        {
            try
            {
                ServerAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1995);
                ClientAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
                //tcp = new TcpClient(AddressFamily.InterNetwork);
                tcp = new TcpClient(ClientAddress);
                udp = new UdpClient(ClientAddress);
            }
            catch
            {
                Console.WriteLine(port);
            }
        }

        public void Connect()
        {
            try
            {
                tcp.Connect(ServerAddress);
                receiveData = tcp.GetStream();
                Array.Resize(ref readBuff, tcp.ReceiveBufferSize);
                receiveData.BeginRead(readBuff, 0, tcp.ReceiveBufferSize, TCPReceive, null);
                Console.WriteLine("Kết nối sv thành công");
                udp.BeginReceive(UDPReceive, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("Khong the ket noi sv");
            }
        }

        private void UDPReceive(IAsyncResult ar)
        {
            byte[] udpReceived = udp.EndReceive(ar, ref endPoint);
            LoginData loginData = new LoginData(udpReceived);
            udp.BeginReceive(UDPReceive, null);
        }

        void UDPSend(byte[] package)
        {
            udp.Send(package);
        }

        private void TCPReceive(IAsyncResult ar)
        {
            try
            {
                int receivedSize = receiveData.EndRead(ar);
                if (tcp == null) return;
                if (receivedSize <= 0)
                {
                    CloseConnection();
                    return;
                }
                tcp.NoDelay = false;
                byte[] receivedData = new byte[receivedSize];
                Buffer.BlockCopy(readBuff, 0, receivedData, 0, receivedSize);
                //Package receivedPackage = receivedData.Deserialize<Package>();
                receiveData.BeginRead(readBuff, 0, tcp.ReceiveBufferSize, TCPReceive, null);
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
            tcp.Close();
        }

        public void TCPSend(Package message)
        {
            tcp.Client.Send(message.Serialize());
            //AddMessage(message);
        }
        public void SendDDos()
        {
            try
            {
                tcp.Client.Send(new byte[256]);
                Console.WriteLine("Sent data");
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot send data");
            }
        }
    }
}
