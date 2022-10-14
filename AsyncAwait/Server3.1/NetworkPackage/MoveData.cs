using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Server.NetworkPackage
{
    [Serializable]
    public class MoveData : Package
    {
        public string id = Guid.NewGuid().ToString();
        public int moveTo = 16;
        public float abc = 12f;
        public double bcd = 13;
        public MoveData()
        {
            packageType = PackageType.MoveData;
        }
        public MoveData(string id, int moveTo) : this()
        {
            this.id = id;
            this.moveTo = moveTo;
        }
        public override string ToString()
        {
            return $"id : {id} - moveTo : {moveTo}";
        }
    }
}

