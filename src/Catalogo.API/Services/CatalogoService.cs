using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Catalogo.API.Models;

namespace Catalogo.API
{
    public class CatalogoService
    {
        private readonly List<Book> catalog;

        public bool IsJsonLoaded { get; private set; }

        public CatalogoService(string jsonFilePath)
        {
            if (string.IsNullOrEmpty(jsonFilePath))
            {
                throw new ArgumentException("O caminho do arquivo JSON não pode ser nulo ou vazio.", nameof(jsonFilePath));
            }

            try
            {
                var jsonString = File.ReadAllText(jsonFilePath);

                catalog = JsonSerializer.Deserialize<List<Book>>(jsonString);
                IsJsonLoaded = true;
            }
            catch (Exception ex)
            {
                IsJsonLoaded = false;
                Console.WriteLine($"Error loading catalog: {ex}");
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
                (book.Specifications.Author != null && book.Specifications.Author.ToLowerInvariant().Contains(searchTerm)) ||
                CheckIllustratorContainsTerm(book.Specifications.Illustrator, searchTerm));
        }

        private bool CheckIllustratorContainsTerm(object illustrator, string searchTerm)
        {
            if (illustrator is string singleIllustrator)
            {
                return singleIllustrator.ToLowerInvariant().Contains(searchTerm);
            }
            else if (illustrator is List<string> multipleIllustrators)
            {
                return multipleIllustrators.Any(i => i.ToLowerInvariant().Contains(searchTerm));
            }

            return false;
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