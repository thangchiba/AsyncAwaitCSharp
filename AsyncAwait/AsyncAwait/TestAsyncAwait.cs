using System;
using System.Text;
using System.Security.Cryptography;

namespace AsyncAwait
{
    public class TestAsyncAwait
    {
        string lastHash = "";
        int count = 0;
        int loopTime = 1000000;

        public TestAsyncAwait()
        {
        }

        public TestAsyncAwait(int loopTime)
        {
            this.loopTime = loopTime;
        }

        public async Task<string> DoAsyncParallel()
        {
            List<Task> listTask = new List<Task>();
            for (int i = 0; i < loopTime; i++)
            {
                var t = Task.Run(() =>
                {
                    lastHash = ComputeSha256Hash(i.ToString());
                    Console.WriteLine(lastHash);
                });
                listTask.Add(t);
            }
            await Task.WhenAll(listTask);
            return lastHash;
        }

        public async Task<string> DoAsync()
        {
            for (int i = 0; i < loopTime; i++)
            {
                await Task.Run(() =>
                {
                    lastHash = ComputeSha256Hash(i.ToString());
                });
            }
            return lastHash;
        }

        public string DoSync()
        {
            for (int i = 0; i < loopTime; i++)
            {
                lastHash = ComputeSha256Hash(i.ToString());
            }
            return lastHash;
        }

        public string ComputeSha256Hash(string rawData)
        {
            rawData = count++.ToString();
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

