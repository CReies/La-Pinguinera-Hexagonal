using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Application.DTOs.Book;

public class BookDTO
{
	public string Id { get; set; }
	public string Title { get; set; }
	public string Author { get; set; }
	public decimal Price { get; set; }
	public BookType Type { get; set; }
}
