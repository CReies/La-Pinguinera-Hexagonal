using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Events.Enums;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Events;

public class IndividualPriceCalculated( string? bookTitle, string? bookAuthor, decimal bookBasePrice, BookType bookType, DateOnly customerRegisterDate ) : DomainEvent( EventType.INDIVIDUAL_PRICE_CALCULATED.ToString() )
{
	public string? Title { get; set; } = bookTitle;
	public string? Author { get; set; } = bookAuthor;
	public decimal BasePrice { get; set; } = bookBasePrice;
	public BookType BookType { get; set; } = bookType;
	public DateOnly CustomerRegisterDate { get; set; } = customerRegisterDate;
}
