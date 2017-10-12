using Newtonsoft.Json;
using System.IO;

namespace SC_AnalysisSystem_Common
{
    public class JsonUtil
    {
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(string json)
        {
            return (T)JsonConvert.DeserializeObject(json, typeof(T));
        }
    }
}
