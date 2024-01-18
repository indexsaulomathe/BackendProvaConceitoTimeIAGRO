using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Catalogo.API.Converters;

namespace Catalogo.API.Models
{
    public class Book
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("specifications")]
        public Specifications Specifications { get; set; }
    }

    public class Specifications
    {
        [JsonPropertyName("Originally published")]
        public string OriginallyPublished { get; set; }

        [JsonPropertyName("Author")]
        public string Author { get; set; }

        [JsonPropertyName("Page count")]
        public int PageCount { get; set; }

        [JsonConverter(typeof(IllustratorConverter))]
        [JsonPropertyName("Illustrator")]
        public List<string> Illustrator { get; set; }

        [JsonConverter(typeof(GenresConverter))]
        [JsonPropertyName("Genres")]
        public List<string> Genres { get; set; }
    }
}