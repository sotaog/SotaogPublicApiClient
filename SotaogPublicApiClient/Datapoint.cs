using Newtonsoft.Json;

namespace SOTAOG.PublicApi
{   
    [JsonConverter(typeof(DatapointConverter))]
    public class Datapoint
    {
        public Datapoint() {}
        public Datapoint(ulong timestamp, float value) {
            Timestamp = timestamp;
            Value = value;
        }
        public ulong Timestamp;
        public float Value;
    }
}