using Newtonsoft.Json;

namespace SharedInformation
{
    public class MessageDataContract
    {
        [JsonProperty("username")]
        public string Username;
        [JsonProperty("message")]
        public string Message;
    }
}
