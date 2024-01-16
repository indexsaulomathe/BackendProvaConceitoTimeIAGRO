using System.Collections.Generic;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public BookSpecifications Specifications { get; set; }
}

public class BookSpecifications
{
    public string OriginallyPublished { get; set; }
    public string Author { get; set; }
    public int PageCount { get; set; }
    public List<string> Illustrators { get; set; }
    public List<string> Genres { get; set; }
}