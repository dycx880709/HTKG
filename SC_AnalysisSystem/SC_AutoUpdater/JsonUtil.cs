using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdater
{
    public static class JsonUtil
    {
        public static T Deserialize<T>(string json)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            T result = (T)ser.ReadObject(stream);
            return result;
        }

        public static string Serialize<T>(T obj)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            ser.WriteObject(stream, obj);
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        public static T DeserializeToFile<T>(string file)
        {
            var json = File.ReadAllText(file);
            return Deserialize<T>(json);
        }
    }
}
