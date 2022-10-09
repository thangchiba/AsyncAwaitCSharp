using System.Runtime.Serialization.Formatters.Binary;
using Server;
using Server.ExtensionMethod;
using Server.NetworkPackage;

internal class Program
{
    private static void Main(string[] args)
    {
        ServerManager server = new ServerManager();
        server.Start();
        //ClientTCP client = new ClientTCP();
        //client.Connect();
        //ClientTCP client2 = new ClientTCP();
        //client2.Connect();
        for (int i = 0; i < 100; i++)
        {
            ClientTCP2 client3 = new ClientTCP2();
            client3.Connect();
        }
        Thread.Sleep(500);
        for (int i = 0; i < 1000; i++)
        {
            server.SendToAllClient(new MoveData());
            Thread.Sleep(50);
        }
        server.SendToAllClient(new MoveData("1", 1));


        Console.ReadKey();
    }
}