using Newtonsoft.Json;

namespace SharedInformation
{
    public static class Serializer
    {
        private static readonly JsonSerializer Serealizer;
        static Serializer()
        {
            Serealizer = JsonSerializer.Create();
        }

        public static T Deserialize<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
