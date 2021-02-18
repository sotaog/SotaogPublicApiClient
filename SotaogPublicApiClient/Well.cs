using Newtonsoft.Json;

namespace SOTAOG.PublicApi
{   
    public class Well
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("label")]
        public string Label;
        [JsonProperty("facility")]
        public string Facility;
        [JsonProperty("datatypes")]
        public string[] Datatypes;
    }
}