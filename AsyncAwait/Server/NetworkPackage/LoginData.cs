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
        public LoginData()
        {
        }
        public LoginData(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public LoginData(byte[] data) : base(data)
        {
        }

        internal override void Deserialize()
        {
            this.userName = ReadString();
            this.password = ReadString();
            //return this;
        }

        public override long GetSize()
        {
            throw new NotImplementedException();
        }

        public override byte[] Serialize()
        {
            Write(userName);
            Write(password);
            return buffer.ToArray();
        }
    }
}

