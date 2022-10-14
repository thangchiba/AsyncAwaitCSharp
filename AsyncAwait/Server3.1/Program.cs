using System;
using System.Runtime.Serialization.Formatters.Binary;
using Server;
using Server.ExtensionMethod;
using Server.NetworkPackage;

namespace RPGServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //ServerManager server = new ServerManager();
            //server.Start();
            //ClientTCP client = new ClientTCP();
            //client.Connect();
            //ClientTCP client2 = new ClientTCP();
            //client2.Connect();
            //for (int i = 0; i < 5000; i++)
            //{
            //    ClientTCP2 client3 = new ClientTCP2();
            //    client3.Connect();
            //}

            ClientTCP2 client4 = new ClientTCP2();
            client4.Connect();
            client4.Send(new LoginData("thangchiba", "123456"));
            //Thread.Sleep(500);
            //for (int i = 0; i < 10000; i++)
            //{
            //    server.SendToAllClient(new MoveData());
            //    Thread.Sleep(1000);
            //}
            //server.SendToAllClient(new MoveData("1", 1));

            //DataConfigure.ConnectToDatabase();
            Console.ReadKey();
        }
    }
}