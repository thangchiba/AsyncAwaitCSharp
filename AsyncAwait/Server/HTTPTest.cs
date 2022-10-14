using System;
using System.Net.NetworkInformation;

namespace Server
{
    public class HTTPTest
    {
        HttpClient client = new HttpClient();
        Ping ping = new Ping();
        string url = "http://tutienproh5.com/";
        public async Task SendRequest()
        {
            await Task.Factory.StartNew(() => { client.GetAsync(url); });
            Console.WriteLine("Sent Request");
        }

        public void SendPing()
        {
            ping.SendAsync(url, null);
        }
    }
}

