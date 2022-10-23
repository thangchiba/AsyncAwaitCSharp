using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace Server.NetworkPackage
{
    [Serializable]
    public class LoginData : Package
    {
        public string userName;
        public string password;
        public override PackageType packageType => PackageType.LoginData;

        public LoginData(string userName, string password) : base()
        {
            this.userName = userName;
            this.password = password;
        }

        public LoginData(byte[] _data) : base(_data)
        {
        }

        internal override void ReadData()
        {
            this.userName = buffer.ReadString();
            this.password = buffer.ReadString();
        }

        internal override void WriteData()
        {
            buffer.Write(userName);
            buffer.Write(password);
        }

        internal override void Execution()
        {
            Console.WriteLine("Run Login");
        }
    }
}

