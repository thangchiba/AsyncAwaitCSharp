using System;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace Server.NetworkPackage
{
    [Serializable]
    public class MoveData : Package
    {
        public int userId;
        public Vector3 position;
        public override PackageType packageType => PackageType.MoveData;

        public MoveData(int userId, Vector3 position) : base()
        {
            this.userId = userId;
            this.position = position;
        }

        public MoveData(byte[] _data) : base(_data)
        {
        }

        internal override void Deserialize()
        {
            this.userId = ReadInt();
            this.position = ReadVector3();
            //return this;
        }

        internal override byte[] Serialize()
        {
            Write(userId);
            Write(position);
            serialized = true;
            return buffer.ToArray();
        }

        internal override void Execution()
        {
            Console.WriteLine("Run Move");
        }
    }
}

