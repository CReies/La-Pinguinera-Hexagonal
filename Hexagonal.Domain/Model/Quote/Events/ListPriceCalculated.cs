using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Events.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Events;

public class ListPriceCalculated( List<(string bookId, int quantity)> booksRequested, DateOnly customerRegisterDate ) : DomainEvent( EventType.LIST_PRICE_CALCULATED.ToString() )
{
	public List<(string bookId, int quantity)> BooksRequested { get; set; } = booksRequested;
	public DateOnly CustomerRegisterDate { get; set; } = customerRegisterDate;
}
