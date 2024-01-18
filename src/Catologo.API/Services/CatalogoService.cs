using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Catalogo.API.Models;


namespace Catalogo.API
{
   public class CatalogoService
{
    private readonly List<Book> catalog;

    public CatalogoService(string jsonFilePath)
    {
        if (string.IsNullOrEmpty(jsonFilePath))
        {
            throw new ArgumentException("O caminho do arquivo JSON não pode ser nulo ou vazio.", nameof(jsonFilePath));
        }

        try
        {
            var jsonString = File.ReadAllText(jsonFilePath);
            catalog = JsonConvert.DeserializeObject<List<Book>>(jsonString);
        }
        catch (Exception ex)
        {
            // Trate a exceção ou lance uma exceção personalizada conforme necessário
            throw new ApplicationException("Erro ao carregar o catálogo.", ex);
        }
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return catalog;
    }

    public IEnumerable<Book> SearchBooks(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return Enumerable.Empty<Book>();
        }

        searchTerm = searchTerm.ToLowerInvariant();

        return catalog.Where(book =>
            book.Name.ToLowerInvariant().Contains(searchTerm) ||
            book.Specifications.Author.ToLowerInvariant().Contains(searchTerm));
    }

    public IEnumerable<Book> SortByPrice(bool ascending)
    {
        return ascending ? catalog.OrderBy(book => book.Price) : catalog.OrderByDescending(book => book.Price);
    }

    public decimal CalculateShippingCost(decimal bookPrice)
    {
        const decimal ShippingRate = 0.2m;
        return bookPrice * ShippingRate;
    }
}
}