using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace Server.NetworkPackage
{
    [Serializable]
    public class LoginData : Package, ISerializable
    {
        public string userName;
        public string password;
        public LoginData()
        {
            packageType = PackageType.LoginData;
        }
        public LoginData(string userName, string password) : this()
        {
            this.userName = userName;
            this.password = password;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("userName", userName);
            info.AddValue("password", password);
        }

        public LoginData(SerializationInfo info, StreamingContext context)
        {
            userName = (string)info.GetValue("userName", typeof(string));
            password = (string)info.GetValue("password", typeof(string));
        }
    }
}

