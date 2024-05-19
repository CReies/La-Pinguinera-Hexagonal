using LaPinguinera.Quotes.Domain.Model.Quote.Entities;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Factory;

public class BookFactory
{
	public AbstractBook Create( string? id, string? title, string? author, decimal basePrice, BookType type )
	{
		Dictionary<BookType, AbstractBook> bookChildren;

		if (id is null)
		{
			bookChildren = new Dictionary<BookType, AbstractBook>
			{
				{BookType.BOOK, Book.From(title, author, basePrice) },
				{BookType.NOVEL, Novel.From(title, author, basePrice) }
			};
		}
		else
		{
			bookChildren = new Dictionary<BookType, AbstractBook>
			{
				{BookType.BOOK, Book.From(id, title, author, basePrice) },
				{BookType.NOVEL, Novel.From(id, title, author, basePrice) }
			};
		}

		var book = bookChildren[type];

		return book ?? throw new ArgumentException( "Book type not found" );
	}
}
