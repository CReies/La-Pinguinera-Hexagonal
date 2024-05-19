using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Events.Enums;
using System.Text.Json.Serialization;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Events;

public class ListPriceCalculated : DomainEvent
{
	public List<(string bookId, int quantity)> BooksRequested { get; set; }
	public DateOnly CustomerRegisterDate { get; set; }

	public ListPriceCalculated( List<(string bookId, int quantity)> booksRequested, DateOnly customerRegisterDate ) : base( EventType.ListPriceCalculated.ToString() )
	{
		BooksRequested = booksRequested;
		CustomerRegisterDate = customerRegisterDate;
	}

	[JsonConstructor]
	public ListPriceCalculated( DateTime moment, int version, string uuid, string type, string aggregateName, string aggregateId, List<(string bookId, int quantity)> booksRequested, DateOnly customerRegisterDate ) : base( moment, version, uuid, type, aggregateName, aggregateId )
	{
		BooksRequested = booksRequested;
		CustomerRegisterDate = customerRegisterDate;
	}
}
