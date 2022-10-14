using System;
using System.Net;
using System.Net.Sockets;
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
            packageType = PackageType.LoginData;
        }
        public LoginData(string userName, string password) : this()
        {
            this.userName = userName;
            this.password = password;
        }
    }
}

