using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Catalogo.API.Converters
{
    public class IllustratorConverter : JsonConverter<List<string>>
    {
        public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                if (doc.RootElement.ValueKind == JsonValueKind.String)
                {
                    string singleIllustrator = doc.RootElement.GetString();
                    return new List<string> { singleIllustrator };
                }
                else if (doc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    return JsonSerializer.Deserialize<List<string>>(doc.RootElement.GetRawText(), options);
                }

                throw new JsonException("Unexpected token type for Illustrator");
            }
        }

        public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
        {
            if (value.Count == 1)
            {
                writer.WriteStringValue(value[0]);
            }
            else
            {
                JsonSerializer.Serialize(writer, value, options);
            }
        }
    }

    public class GenresConverter : JsonConverter<List<string>>
    {
        public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                if (doc.RootElement.ValueKind == JsonValueKind.String)
                {
                    return new List<string> { doc.RootElement.GetString() };
                }
                else if (doc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    return JsonSerializer.Deserialize<List<string>>(doc.RootElement.GetRawText(), options);
                }

                throw new JsonException("Unexpected token type for Genres");
            }
        }

        public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
        {
            if (value.Count == 1)
            {
                writer.WriteStringValue(value[0]);
            }
            else
            {
                JsonSerializer.Serialize(writer, value, options);
            }
        }
    }
}