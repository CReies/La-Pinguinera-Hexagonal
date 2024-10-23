using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Entities;

public class Novel : AbstractBook
{
	private Novel( BookId id, Data data, BaseIncrease baseIncrease, BasePrice basePrice ) :
		base( id, data, baseIncrease, basePrice )
	{ BaseIncrease = BaseIncrease.Of( 1m ); }

	private Novel( Data data, BaseIncrease baseIncrease, BasePrice basePrice ) :
		this( new(), data, baseIncrease, basePrice )
	{ BaseIncrease = BaseIncrease.Of( 1m ); }

	public static Novel From( string? title, string? author, decimal basePrice ) => new
		(
			Data.Of( title, author, BookType.NOVEL ),
			BaseIncrease.Of( 1m ),
			BasePrice.Of( basePrice )
		);

	public static Novel From( string? id, string? title, string? author, decimal basePrice ) => new
		(
			BookId.Of( id ),
			Data.Of( title, author, BookType.NOVEL ),
			BaseIncrease.Of( 1m ),
			BasePrice.Of( basePrice )
		);

	public override AbstractBook Clone() => (Novel)MemberwiseClone();
}
