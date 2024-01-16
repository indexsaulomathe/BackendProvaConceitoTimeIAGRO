using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CatalogoController : ControllerBase
{
    private readonly CatalogService catalogService;

    public CatalogoController(CatalogService catalogService)
    {
        this.catalogService = catalogService;
    }

    [HttpGet("books")]
    public IActionResult GetAllBooks()
    {
        var books = catalogService.GetAllBooks();
        return Ok(books);
    }

    [HttpGet("search")]
    public IActionResult SearchBooks(string searchTerm)
    {
        var books = catalogService.SearchBooks(searchTerm);
        return Ok(books);
    }

    [HttpGet("sort")]
    public IActionResult SortBooks(bool ascending)
    {
        var books = catalogService.SortByPrice(ascending);
        return Ok(books);
    }

    [HttpGet("shipping")]
    public IActionResult CalculateShipping(decimal bookPrice)
    {
        var shippingCost = catalogService.CalculateShippingCost(bookPrice);
        return Ok(shippingCost);
    }
}