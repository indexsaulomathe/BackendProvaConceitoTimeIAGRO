using System;
using System.Collections.Generic;
using System.Linq;
using Catalogo.API;
using Catalogo.API.Models;

namespace Catalogo.Tests
{
    public class AssertionFailedException : Exception
    {
        public AssertionFailedException(string message) : base(message)
        {
        }
    }

    public class CatalogoServiceTests
    {
        private CatalogoService catalogoService;

        public CatalogoServiceTests()
        {
            InitializeCatalogoService();
        }

        private void InitializeCatalogoService()
        {
            var jsonFilePath = "books.json";
            catalogoService = new CatalogoService(jsonFilePath);
        }

        private void Assert(bool condition, string message)
        {
            if (!condition)
            {
                throw new AssertionFailedException(message);
            }
        }

        private void AssertIsNotEmpty<T>(IEnumerable<T> collection, string message)
        {
            if (collection == null || !collection.Any())
            {
                throw new AssertionFailedException(message);
            }
        }

        private void AssertAreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, string message)
        {
            if (!expected.SequenceEqual(actual))
            {
                throw new AssertionFailedException(message);
            }
        }

        public void TestGetAllBooks()
        {
            var result = catalogoService.GetAllBooks();
            AssertIsNotEmpty(result, "TestGetAllBooks failed: Result is null or empty.");
        }

        public void TestSearchBooks()
        {
            string searchTerm = "Harry Potter";
            var result = catalogoService.SearchBooks(searchTerm);

            AssertIsNotEmpty(result, $"TestSearchBooks failed: Result for search term '{searchTerm}' is null or empty.");
            Assert(result.All(book => book.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)),
                $"TestSearchBooks failed: Not all books in the result contain the search term '{searchTerm}'.");
        }

        public void TestSortBooks()
        {
            var resultAsc = catalogoService.SortByPrice(true).ToList();
            var resultDesc = catalogoService.SortByPrice(false).ToList();

            AssertIsNotEmpty(resultAsc, "Ascending result should not be null or empty.");
            AssertIsNotEmpty(resultDesc, "Descending result should not be null or empty.");

            for (int i = 0; i < resultAsc.Count - 1; i++)
            {
                Assert(resultAsc[i].Price <= resultAsc[i + 1].Price, "TestSortBooks failed: Ascending list is not correctly sorted.");
            }

            for (int i = 0; i < resultDesc.Count - 1; i++)
            {
                Assert(resultDesc[i].Price >= resultDesc[i + 1].Price, "TestSortBooks failed: Descending list is not correctly sorted.");
            }
        }

        public void TestCalculateShippingCost()
        {
            decimal bookPrice = 20.0m;
            var shippingCost = catalogoService.CalculateShippingCost(bookPrice);

            Assert(shippingCost == bookPrice * 0.2m, "TestCalculateShippingCost failed: Shipping cost calculation is incorrect.");
        }
    }
}