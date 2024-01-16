using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CatalogService
{
    private readonly List<Book> catalog;

    public CatalogService(string jsonFilePath)
    {
        var jsonString = File.ReadAllText(jsonFilePath);
        catalog = JsonSerializer.Deserialize<List<Book>>(jsonString);
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return catalog;
    }

    public IEnumerable<Book> SearchBooks(string searchTerm)
    {
        return catalog.Where(book => book.Title.Contains(searchTerm) || book.Specifications.Author.Contains(searchTerm));
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