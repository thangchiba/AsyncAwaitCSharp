using System;
using System.Net.NetworkInformation;

namespace Server
{
    public class HTTPTest
    {
        HttpClient client = new HttpClient();
        object lockob = new object();
        int count = 0;
        string url = "tlhuuduyen.info";
        public async Task SendRequest()
        {
            await Task.Factory.StartNew(() => { client.GetAsync(url); });
            Console.WriteLine("Sent Request");
        }

        public void SendPing()
        {
            while (true)
            {
                Ping ping = new Ping();
                try
                {
                    var rep = ping.Send(url);
                    //lock (lockob) count++;
                    //Console.WriteLine(count);
                }
                finally
                {
                    ping.Dispose();
                }
            }
        }
    }
}

