using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Factory;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;
using LaPinguinera.Quotes.Domain.Model.Quote.Shared;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.CreationQuoteCalculate;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Entities;

public class CreationQuoteCalculate : Entity<CreationQuoteCalculateId>
{
	private CreationQuoteCalculate( CreationQuoteCalculateId id ) : base( id )
	{ }

	public CreationQuoteCalculate() : this( new() )
	{ }

	public AbstractBook Calculate( string bookId, string title, string author, decimal basePrice, BookType type )
	{
		IResult result = new Result();

		BookFactory _bookFactory = new();
		AbstractBook book = _bookFactory.Create( bookId, title, author, basePrice, type );
		book.CalculateSellPrice();

		result.Quotes[0].Books.Add( book );

		//quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );

		return book;
	}
}
