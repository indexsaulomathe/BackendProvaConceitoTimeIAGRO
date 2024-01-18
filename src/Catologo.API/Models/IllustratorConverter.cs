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
                // Se for uma matriz, converte para uma lista de strings
                return token.ToObject<List<string>>();
            }
            else if (token.Type == JTokenType.String)
            {
                // Se for uma string, cria uma lista de strings com um único elemento
                string singleIllustrator = token.ToObject<string>();
                return new List<string> { singleIllustrator };
            }

            // Se não for nem uma matriz nem uma string, lança uma exceção
            throw new JsonSerializationException("Unexpected token type for Illustrator");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            List<string> list = (List<string>)value;
            if (list.Count == 1)
            {
                // If there's only one item in the list, write it as a single string
                writer.WriteValue(list[0]);
            }
            else
            {
                // Otherwise, write the list as a JSON array
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