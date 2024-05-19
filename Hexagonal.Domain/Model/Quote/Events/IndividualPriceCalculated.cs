using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Events.Enums;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;
using System.Text.Json.Serialization;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Events;

public class IndividualPriceCalculated : DomainEvent
{
	public string? BookId { get; set; }
	public string? Title { get; set; }
	public string? Author { get; set; }
	public decimal BasePrice { get; set; }
	public BookType BookType { get; set; }

	public IndividualPriceCalculated( string? title, string? author, decimal basePrice, BookType bookType ) : base( EventType.IndividualPriceCalculated.ToString() )
	{
		Title = title;
		Author = author;
		BasePrice = basePrice;
		BookType = bookType;
	}

	public IndividualPriceCalculated( string? bookId, string? title, string? author, decimal basePrice, BookType bookType ) : base( EventType.IndividualPriceCalculated.ToString() )
	{
		BookId = bookId;
		Title = title;
		Author = author;
		BasePrice = basePrice;
		BookType = bookType;
	}

	[JsonConstructor]
	public IndividualPriceCalculated( DateTime moment, int version, string uuid, string type, string aggregateName, string aggregateId, string? bookId, string? title, string? author, decimal basePrice, BookType bookType ) : base( moment, version, uuid, type, aggregateName, aggregateId )
	{
		BookId = bookId;
		Title = title;
		Author = author;
		BasePrice = basePrice;
		BookType = bookType;
	}
}
