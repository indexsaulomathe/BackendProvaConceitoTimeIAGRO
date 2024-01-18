using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Catalogo.API.Converters
{
    public class IllustratorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<string>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);

            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<string>>();
            }
            else if (token.Type == JTokenType.String)
            {
                string singleIllustrator = token.ToObject<string>();
                return new List<string> { singleIllustrator };
            }

            throw new JsonSerializationException("Unexpected token type for Illustrator");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            List<string> list = (List<string>)value;
            if (list.Count == 1)
            {
                writer.WriteValue(list[0]);
            }
            else
            {
                writer.WriteStartArray();
                foreach (var item in list)
                {
                    writer.WriteValue(item);
                }
                writer.WriteEndArray();
            }
        }
    }
}

public class GenresConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(List<string>));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        switch (reader.TokenType)
        {
            case JsonToken.String:
                return new List<string> { serializer.Deserialize<string>(reader) };
            case JsonToken.StartArray:
                return serializer.Deserialize<List<string>>(reader);
            default:
                throw new JsonSerializationException("Expected string or array");
        }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        List<string> list = (List<string>)value;
        if (list.Count == 1)
        {
            serializer.Serialize(writer, list[0]);
        }
        else
        {
            serializer.Serialize(writer, list);
        }
    }
}