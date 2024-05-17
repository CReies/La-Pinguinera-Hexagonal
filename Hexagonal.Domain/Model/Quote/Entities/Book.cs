using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Entities;

public class Book : AbstractBook
{
	private Book( BookId id, Data data, BaseIncrease baseIncrease, BasePrice basePrice ) :
		base( id, data, baseIncrease, basePrice )
	{ BaseIncrease = BaseIncrease.Of( 1m / 3m ); }

	private Book( Data data, BaseIncrease baseIncrease, BasePrice basePrice ) :
		this( new(), data, baseIncrease, basePrice )
	{ BaseIncrease = BaseIncrease.Of( 1m / 3m ); }

	public static Book From( string? title, string? author, decimal basePrice ) => new
		(
			Data.Of( title, author, BookType.BOOK ),
			BaseIncrease.Of( 1m / 3m ),
			BasePrice.Of( basePrice )
		);

	public static Book From( string? id, string? title, string author, decimal basePrice ) => new
		(
			BookId.Of( id ),
			Data.Of( title, author, BookType.BOOK ),
			BaseIncrease.Of( 1m / 3m ),
			BasePrice.Of( basePrice )
		);
}
