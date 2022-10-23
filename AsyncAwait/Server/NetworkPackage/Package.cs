using System;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Server.ExtensionMethod;

namespace Server.NetworkPackage
{
    [Serializable]
    public abstract class Package
    {
        public abstract PackageType packageType { get; }
        internal ByteBuffer buffer;
        private bool serialized = false;
        internal abstract void WriteData();
        internal abstract void ReadData();
        internal abstract void Execution();

        public byte[] Serialize()
        {
            if (!serialized)
            {
                WriteData();
                serialized = true;
            }
            return buffer.ToArray();
        }

        protected Package()
        {
            buffer = new ByteBuffer();
            buffer.Write((byte)packageType);
        }

        internal Package(int _id) : this()
        {
            buffer.Write(_id);
        }

        public Package(byte[] data)
        {
            buffer = new ByteBuffer(data);
            ReadData();
        }

        public Type GetPackageType()
        {
            if (buffer.readableBuffer[0] == (byte)PackageType.LoginData)
                return typeof(LoginData);
            if (buffer.readableBuffer[0] == (byte)PackageType.MoveData)
                return typeof(MoveData);
            else return null;
        }

        public void WriteSomething()
        {
            Console.WriteLine("Write something");
        }

    }
}
