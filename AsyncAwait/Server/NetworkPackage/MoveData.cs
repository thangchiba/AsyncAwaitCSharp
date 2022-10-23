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
        public ushort userId;
        public Vector3 position;
        public override PackageType packageType => PackageType.MoveData;

        public MoveData(ushort userId, Vector3 position) : base()
        {
            this.userId = userId;
            this.position = position;
        }

        public MoveData(byte[] _data) : base(_data)
        {
        }

        internal override void ReadData()
        {
            this.userId = buffer.ReadUShort();
            this.position = buffer.ReadVector3();
        }

        internal override void WriteData()
        {
            buffer.Write(userId);
            buffer.Write(position);
        }

        internal override void Execution()
        {
            Console.WriteLine("Run Move " + position.X + " - " + position.Y + " - " + position.Z);
        }
    }
}

