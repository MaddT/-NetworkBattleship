using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NetworkGame
{
    //методы для сериализации классов
    public static class SerializationUtils
    {
        public static object DeSerialization(byte[] serializedAsBytes)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Write(serializedAsBytes, 0, serializedAsBytes.Length);
            stream.Seek(3, SeekOrigin.Begin);
            object mess = formatter.Deserialize(stream);
            return mess;
        }

        public static byte[] Serialization(byte[] Code, Object obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            stream.Write(Code, 0, Code.Length);
            formatter.Serialize(stream, obj);
            byte[] msg = stream.ToArray();
            return msg;
        }
    }
}
