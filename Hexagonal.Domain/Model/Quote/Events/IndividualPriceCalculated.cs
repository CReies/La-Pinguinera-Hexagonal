using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Events.Enums;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Events;

public class IndividualPriceCalculated( string? title, string? author, decimal basePrice, BookType bookType ) : DomainEvent( EventType.INDIVIDUAL_PRICE_CALCULATED.ToString() )
{
	public string? Title { get; set; } = title;
	public string? Author { get; set; } = author;
	public decimal BasePrice { get; set; } = basePrice;
	public BookType BookType { get; set; } = bookType;
}
