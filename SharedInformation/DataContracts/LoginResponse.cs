using Newtonsoft.Json;

namespace SharedInformation.DataContracts
{
    public class LoginResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
