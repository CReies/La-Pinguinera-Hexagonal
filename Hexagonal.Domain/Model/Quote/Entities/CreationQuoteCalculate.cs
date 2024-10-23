using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Factory;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.CreationQuoteCalculate;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Entities;

public class CreationQuoteCalculate : Entity<CreationQuoteCalculateId>
{
	public Result Result { get; private set; }
	private CreationQuoteCalculate( CreationQuoteCalculateId id ) : base( id )
	{ }

	public CreationQuoteCalculate() : this( new() )
	{ }

	public AbstractBook Calculate( string bookId, string title, string author, decimal basePrice, BookType type )
	{

		BookFactory _bookFactory = new();
		AbstractBook book = _bookFactory.Create( bookId, title, author, basePrice, type );
		book.CalculateSellPrice();

		Result = Result.Of( book.Id.Value, book.Data.Value.Title, book.Data.Value.Author, book.BasePrice.Value, book.SellPrice.Value, book.Data.Value.Type, book.Discount!.Value, book.Increase!.Value );

		return book;
	}
}
