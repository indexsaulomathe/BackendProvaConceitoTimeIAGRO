using System;
using System.Collections.Generic;
using Catalogo.API.Controllers;
using Catalogo.API.Models;
using Catalogo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Tests
{
    public class CatalogoControllerTests
    {
        public void GetAllBooks_ReturnsListOfBooks()
        {
            // Arrange
            var mockCatalogoService = new MockCatalogoService();
            var controller = new CatalogoController(mockCatalogoService);

            // Act
            var result = controller.GetAllBooks();

            // Assert
            if (result is IEnumerable<Book>)
            {
                Console.WriteLine("GetAllBooks_ReturnsListOfBooks test passed.");
            }
            else
            {
                Console.WriteLine("GetAllBooks_ReturnsListOfBooks test failed.");
            }
        }

        public void SearchBooks_ReturnsListOfBooks()
        {
            // Arrange
            var mockCatalogoService = new MockCatalogoService();
            var controller = new CatalogoController(mockCatalogoService);
            var searchTerm = "Jules";

            // Act
            var result = controller.SearchBooks(searchTerm);

            // Assert
            if (result is IEnumerable<Book>)
            {
                Console.WriteLine("SearchBooks_ReturnsListOfBooks test passed.");
            }
            else
            {
                Console.WriteLine("SearchBooks_ReturnsListOfBooks test failed.");
            }
        }

        public void SortBooks_ReturnsListOfBooks()
        {
            // Arrange
            var mockCatalogoService = new MockCatalogoService();
            var controller = new CatalogoController(mockCatalogoService);

            // Act
            var result = controller.SortBooks();

            // Assert
            if (result is IEnumerable<Book>)
            {
                Console.WriteLine("SortBooks_ReturnsListOfBooks test passed.");
            }
            else
            {
                Console.WriteLine("SortBooks_ReturnsListOfBooks test failed.");
            }
        }

        public void CalculateShippingCost_ReturnsDecimal()
        {
            // Arrange
            var mockCatalogoService = new MockCatalogoService();
            var controller = new CatalogoController(mockCatalogoService);
            var bookPrice = 10.0m;

            // Act
            var result = controller.CalculateShippingCost(bookPrice);

            // Assert
            if (result is decimal)
            {
                Console.WriteLine("CalculateShippingCost_ReturnsDecimal test passed.");
            }
            else
            {
                Console.WriteLine("CalculateShippingCost_ReturnsDecimal test failed.");
            }
        }
    }

    // Simulação de um serviço Catalogo para testes
    public class MockCatalogoService : CatalogoService
    {
        public MockCatalogoService() : base("caminho_do_arquivo.json")
        {
            // Pode adicionar lógica específica de teste aqui, se necessário
        }
    }
}