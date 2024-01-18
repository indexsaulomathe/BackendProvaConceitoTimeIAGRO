using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Catalogo.API.Models;


namespace Catalogo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogoController : ControllerBase
    {
        private readonly CatalogoService catalogoService;

        public CatalogoController(CatalogoService catalogoService)
        {
            this.catalogoService = catalogoService;
        }

        [HttpGet("all-books")]
        public IEnumerable<Book> GetAllBooks()
        {
            return catalogoService.GetAllBooks();
        }

        [HttpGet("search")]
        public IEnumerable<Book> SearchBooks([FromQuery] string searchTerm)
        {
            return catalogoService.SearchBooks(searchTerm);
        }

        [HttpGet("sort")]
        public IEnumerable<Book> SortBooks([FromQuery] bool ascending = true)
        {
            return catalogoService.SortByPrice(ascending);
        }

        [HttpGet("shipping-cost")]
        public decimal CalculateShippingCost([FromQuery] decimal bookPrice)
        {
            return catalogoService.CalculateShippingCost(bookPrice);
        }
    }
}