using LaPinguinera.Quotes.Domain.Model.Quote.Entities;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Factory;

public class BookFactory
{
	public AbstractBook Create( string? title, string? author, decimal basePrice, BookType type )
	{
		var bookChildren = new Dictionary<BookType, AbstractBook>
		{
			{BookType.BOOK, Book.From(title, author, basePrice) },
			{BookType.NOVEL, Novel.From(title, author, basePrice) }
		};

		var book = bookChildren[type];

		return book ?? throw new ArgumentException( "Book type not found" );
	}
}
