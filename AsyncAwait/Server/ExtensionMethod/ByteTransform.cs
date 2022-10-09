using System;
using System.Runtime.Serialization.Formatters.Binary;
using Server.NetworkPackage;

namespace Server.ExtensionMethod
{
    public static class ByteTransform
    {
        public static byte[] Serialize(this Package obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                bf.Serialize(stream, obj);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error When Serialize File!!!");
                Console.WriteLine(e.ToString());
            }
            return stream.ToArray();
        }

        public static T? Deserialize<T>(this byte[] byteArray)
        {
            MemoryStream stream = new MemoryStream(byteArray);
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                return (T)bf.Deserialize(stream);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error When Serialize File!!!");
                return default(T);
            }
        }
    }
}

