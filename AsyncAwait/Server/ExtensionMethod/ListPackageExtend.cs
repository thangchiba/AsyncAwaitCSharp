using System;
using System.Runtime.Serialization.Formatters.Binary;
using Server.NetworkPackage;

namespace Server.ExtensionMethod
{
    public static class ListPackageExtend
    {
        public static byte[]? Serialize(this List<Package> listPackage)
        {
            try
            {
                List<byte> buffer = new List<byte>();
                //bf.Serialize(stream, obj);
                foreach (Package package in listPackage)
                {
                    byte[] addBytes = package.GetBytes();
                    buffer.AddRange(BitConverter.GetBytes(addBytes.Length));
                    buffer.AddRange(addBytes);
                }
                return buffer.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error When Serialize File!!!");
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static List<Package>? Deserialize(this byte[] buffer)
        {
            MemoryStream stream = new MemoryStream(buffer);
            BinaryFormatter bf = new BinaryFormatter();
            List<Package> result = new List<Package>();
            int readPos = 0;
            try
            {
                //return (T)bf.Deserialize(stream);
                while (readPos < buffer.Length)
                {
                    int packetLength = BitConverter.ToInt32(buffer, readPos);
                    readPos += 4;
                    byte[] packageBuffer = new byte[packetLength];
                    Buffer.BlockCopy(buffer, readPos, packageBuffer, 0, packetLength);
                    result.Add(DetectPackage(packageBuffer));
                    readPos += packetLength;
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error When Serialize File!!!");
                return null;
            }
        }

        static IEnumerator<byte> GetByteInRange(this byte[] buffer, int startIndex, int endIndex)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                yield return buffer[i];
            }
        }

        static Package DetectPackage(byte[] buffer)
        {
            if (buffer[0] == (byte)PackageType.LoginData)
            {
                return new LoginData(buffer);
            }
            if (buffer[0] == (byte)PackageType.MoveData)
            {
                return new MoveData(buffer);
            }
            else return null;
        }
    }
}

