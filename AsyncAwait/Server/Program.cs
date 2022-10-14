using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json.Serialization;
using Server;
using Server.ExtensionMethod;
using Server.NetworkPackage;

internal class Program
{
    private static void Main(string[] args)
    {
        ServerManager server = new ServerManager();
        server.Start();

        //Console.WriteLine(ASCIIEncoding.ASCII.GetBytes("thangchiba").Length);
        //Console.WriteLine(new LoginData("thangchiba", "123456").GetSize());

        //ClientTCP client = new ClientTCP();
        //client.Connect();
        //ClientTCP client2 = new ClientTCP();
        //client2.Connect();

        Console.WriteLine("Nhap so client");
        int clientQuantity = int.Parse(Console.ReadLine());

        Thread t = new Thread(() =>
        {
            for (int i = 0; i < clientQuantity; i++)
            {
                ClientTCP2 client3 = new ClientTCP2();
                client3.Connect();
                Thread.Sleep(1);
            }
        });
        t.IsBackground = true;
        t.Start();

        //ClientTCP2 client4 = new ClientTCP2();
        //client4.Connect();
        //client4.Send(new LoginData("thangchiba", "123456"));
        //Thread.Sleep(500);

        for (int i = 0; i < 100000; i++)
        {
            //server.SendToAllClient(new LoginData("thangchiba", "123456"));
            Task.Factory.StartNew(() => server.SendToAllClient(ASCIIEncoding.ASCII.GetBytes("thangchiba")));
            Thread.Sleep(25);
        }


        //server.SendToAllClient(new MoveData("1", 1));

        //DataConfigure.ConnectToDatabase();
        Console.ReadKey();
    }

}