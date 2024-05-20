using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Shared.Helper;

public class BookResult( string id, string title, string author, decimal basePrice, decimal finalPrice, BookType bookType, decimal discount, decimal increase )
{
	public string Id { get; set; } = id;
	public string Title { get; set; } = title;
	public string Author { get; set; } = author;
	public decimal BasePrice { get; set; } = basePrice;
	public decimal FinalPrice { get; set; } = finalPrice;
	public BookType BookType { get; set; } = bookType;
	public decimal Discount { get; set; } = discount;
	public decimal Increase { get; set; } = increase;
}
