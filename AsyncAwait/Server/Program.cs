using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json.Serialization;
using Server;
using Server.ExtensionMethod;
using Server.NetworkPackage;

internal class Program
{
    static ServerManager server;
    private static void Main(string[] args)
    {
        server = new ServerManager();
        server.Start();

        //Console.WriteLine(ASCIIEncoding.ASCII.GetBytes("thangchiba,123456").Length);
        //var con = BitConverter.GetBytes(444);
        //Console.WriteLine(con.ToString());
        //Console.WriteLine(BitConverter.ToInt16(con, 0));
        //Console.WriteLine(new LoginData("thangchiba", "123456").GetSize());
        for(int i = 0; i < 100; i++)
        {
            ClientTCP client4 = new ClientTCP();
            client4.Connect();
            client4.Send(new LoginData("thangchiba"+i, "12345678"));
        }


        ClientTCP client5 = new ClientTCP();
        client5.Connect();
        client5.Send(new LoginData("client5", "12345678"));
        //Thread.Sleep(500);
        //byte[] a = new byte[1024];
        //var a = new LoginData("hoangthang", "123456");
        //var b = new LoginData(a.Serialize());
        Console.ReadKey();
    }


    static void SendMultiPackage()
    {
        Console.WriteLine("Nhap so client");
        int clientQuantity = int.Parse(Console.ReadLine());

        Thread t = new Thread(() =>
        {
            for (int i = 0; i < clientQuantity; i++)
            {
                ClientTCP client3 = new ClientTCP();
                client3.Connect();
                Thread.Sleep(1);
            }
        });
        t.IsBackground = true;
        t.Start();

        for (int i = 0; i < 100000; i++)
        {
            //server.SendToAllClient(new LoginData("thangchiba", "123456"));
            Task.Factory.StartNew(() => server.SendToAllClient(ASCIIEncoding.ASCII.GetBytes("thangchiba")));
            Thread.Sleep(25);
        }
    }
}