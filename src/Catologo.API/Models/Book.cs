using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Catalogo.API.Converters; // Adicionado


namespace Catalogo.API.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Specifications Specifications { get; set; }
    }

    public class Specifications
    {
        public string OriginallyPublished { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }

        [JsonConverter(typeof(IllustratorConverter))]
        public List<string> Illustrator { get; set; }
        
        [JsonConverter(typeof(GenresConverter))]
        public List<string> Genres { get; set; }
    }
}