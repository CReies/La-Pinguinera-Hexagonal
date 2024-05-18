using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;

public class Data : IValueObject<(string Title, string Author, BookType Type)>
{
	public (string Title, string Author, BookType Type) Value { get; private set; }

	private Data( (string? Title, string? Author, BookType Type) value )
	{
		if (string.IsNullOrWhiteSpace( value.Title ))
		{
			throw new ArgumentException( "Title cannot be null or empty" );
		}

		if (string.IsNullOrWhiteSpace( value.Author ))
		{
			throw new ArgumentException( "Author cannot be null or empty" );
		}

		Value = value!;
	}

	public static Data Of( string? Title, string? Author, BookType Type )
	{
		return new Data( (Title, Author, Type) );
	}
}
