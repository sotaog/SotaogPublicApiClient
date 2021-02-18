using Newtonsoft.Json;
namespace SOTAOG.PublicApi
{ 
    public class JWT
    {
        [JsonProperty("access_token")]
        public string AccessToken;
        [JsonProperty("expires_in")]
        public int ExpiresIn;
        [JsonProperty("token_type")]
        public string TokenType;
    }
}