﻿
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Server.ExtensionMethod;
using Server.NetworkPackage;

namespace Server
{
    public class ClientTCP
    {
        IPEndPoint ServerIP;
        public TcpClient client;
        NetworkStream receiveData;
        private byte[] readBuff = new byte[512];

        public void Connect()
        {
            ServerIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1995);
            //ServerIP = new IPEndPoint(IPAddress.Parse("108.162.193.113"), 8080);
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
                //Package receivedPackage = receivedData.Deserialize<Package>();
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
            client.Close();
        }

        public void Send(Package message)
        {
            client.Client.Send(message.Serialize());
            //AddMessage(message);
        }
        public void SendDDos()
        {
            try
            {
                client.Client.Send(new byte[256]);
                Console.WriteLine("Sent data");
            }
            catch(Exception e)
            {
                Console.WriteLine("Cannot send data");
            }
        }
    }
}
