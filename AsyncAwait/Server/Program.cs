using System;
using System.Numerics;
using System.Reflection;
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
        //ServerManager.Instance.Start();
        //Console.WriteLine("Server started");

        //Client client = new Client(19996);
        //client.Connect();
        //Thread.Sleep(500);
        //client.UDPSend(new MoveData(25, new Vector3(27f, 73f, 82.34f)).Serialize());

        TestList();

        //Console.WriteLine(ASCIIEncoding.ASCII.GetBytes("thangchiba,123456").Length);
        //var con = BitConverter.GetBytes(444);
        //Console.WriteLine(con.ToString());
        //Console.WriteLine(BitConverter.ToInt16(con, 0));
        //Console.WriteLine(new LoginData("thangchiba", "123456").GetSize());


        //LoginData package = new LoginData("thangchiba", "12345678");
        //PropertyInfo[] props = typeof(LoginData).GetProperties();
        //foreach(var prop in props)
        //{
        //    Console.WriteLine($"{prop.Name} : {prop.GetValue(package)}");
        //}


        //for (int i = 0; i < 1; i++)
        //{
        //    Client client4 = new Client(1996);
        //    client4.Connect();
        //    client4.Send(new LoginData("thangchiba" + i, "12345678"));
        //}
        //ClientTCP client5 = new ClientTCP();
        //client5.Connect();
        //client5.Send(new LoginData("client5", "12345678"));
        //for (int i = 0; i < 100000; i++)
        //{
        //    Task.Factory.StartNew(() => ServerManager.SendToAllClient(new LoginData("thangchiba", "12345678")));
        //    Thread.Sleep(50);
        //}




        //var login = new LoginData("hoangthang", "123456");
        //var se = login.GetBytes();
        //Package receivePack = null;
        //if (se[0] == (byte)PackageType.LoginData)
        //{
        //    receivePack = new LoginData(se);
        //}
        //if (se[0] == (byte)PackageType.MoveData)
        //{
        //    receivePack = new MoveData(se);
        //}
        ////var move = new MoveData(25, new Vector3(27f, 73f, 82.34f));
        ////var b = new LoginData(a.Serialize());

        ////var c = move.Serialize();
        //receivePack?.Execution();



        //var login2 = new LoginData("hoangthang", "123456");
        //var move = new MoveData(25, new Vector3(27f, 73, 82.34f));

        //var testResult = listPack.Serialize();
        //var DeseTest = testResult.Deserialize();
        //SendMultiPackage();
        Console.ReadKey();
    }

    private static void TestList()
    {
        //List<string> list1 = new List<string>() { "string1", "string2" };
        //List<string> list2 = new List<string>();
        //list2.Add(list1[0]);
        //list2[0] = "string3";
        //Console.WriteLine(list1[0]);

        //HTTPTest httpTest = new HTTPTest();

        //Thread t = new Thread(() =>
        //{
        //    //for (int i = 0; i < clientQuantity; i++)
        //    while (true)
        //    {
        //        Task.Factory.StartNew(() => httpTest.SendRequest());
        //        Thread.Sleep(10);
        //    }
        //});
        //t.IsBackground = true;
        //t.Start();


        Package pack = new MoveData(25, new Vector3(27f, 73f, 82.34f));
        var a = pack.Serialize();
        Package pack2 = new MoveData(a);
        pack.Execution();
    }

    static void SendMultiPackage()
    {
        Console.WriteLine("Nhap so client");
        int clientQuantity = int.Parse(Console.ReadLine());
        int clientCount = 0;
        Thread t = new Thread(() =>
        {
            //for (int i = 0; i < clientQuantity; i++)
            while (clientCount < clientQuantity)
            {
                Client client3 = new Client(1996 + clientCount);
                client3.Connect();
                clientCount++;
                Thread.Sleep(10);
            }
        });
        t.IsBackground = true;
        t.Start();

        byte[] package = new LoginData("thangchiba", "12345678").Serialize();
        for (int i = 0; i < 1000000; i++)
        {
            //Task.Factory.StartNew(() => ServerManager.Instance.TCPSendToAllClient(new LoginData("thangchiba", "12345678").Serialize()));
            Task.Factory.StartNew(() =>
            {
                List<Package> listPack = new List<Package>();
                for (int i = 0; i < clientQuantity; i++)
                {
                    listPack.Add(new MoveData(25, new Vector3(27f, 73, 82.34f)));
                }
                ServerManager.Instance.UDPSendToAllClient(listPack.Serialize());
            });
            Thread.Sleep(5);
        }
    }


    static void TestClient()
    {

        Client client = new Client(1996);
        client.Connect();

        Thread.Sleep(2500);
        while (true)
        {
            client.TCPSend(new MoveData(25, new Vector3(27f, 73f, 82.34f)));
            Console.WriteLine("sent");
            Thread.Sleep(25);
        }
    }
}