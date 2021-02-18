using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace SOTAOG.PublicApi
{   
    public class DatapointConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Datapoint);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) {
                return null;
            }
            JArray array = JArray.Load(reader);
            Datapoint datapoint = (existingValue as Datapoint ?? new Datapoint());
            datapoint.Timestamp = (ulong) array[0];
            datapoint.Value = (float) array[1];
            return datapoint;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Datapoint datapoint = (Datapoint)value;
            serializer.Serialize(writer, new object[] { datapoint.Timestamp, datapoint.Value });
        }
    }
}