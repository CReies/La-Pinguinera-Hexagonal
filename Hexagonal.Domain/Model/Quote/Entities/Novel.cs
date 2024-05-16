using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Entities;

public class Novel : AbstractBook
{
	private Novel( BookId id, Data data, PriceModifiers priceModifiers, BaseIncrease baseIncrease, BasePrice basePrice, FinalPrice finalPrice ) :
		base( id, data, priceModifiers, baseIncrease, basePrice, finalPrice )
	{ BaseIncrease = BaseIncrease.Of( 1m ); }

	private Novel( Data data, PriceModifiers priceModifiers, BaseIncrease baseIncrease, BasePrice basePrice, FinalPrice finalPrice ) :
		this( new(), data, priceModifiers, baseIncrease, basePrice, finalPrice )
	{ BaseIncrease = BaseIncrease.Of( 1m ); }

	public static Novel From( string? title, string? author, decimal basePrice ) => new
			(
				Data.Of( title, author, 0, BookType.NOVEL ),
				PriceModifiers.Of( 0, 0, 0 ),
				BaseIncrease.Of( 0 ),
				BasePrice.Of( basePrice ),
				FinalPrice.Of( 0 )
			);

	public static Novel From( string? id, string? title, string? author, decimal basePrice ) => new
		(
			BookId.Of( id ),
			Data.Of( title, author, 0, BookType.NOVEL ),
			PriceModifiers.Of( 0, 0, 0 ),
			BaseIncrease.Of( 0 ),
			BasePrice.Of( basePrice ),
			FinalPrice.Of( 0 )
		);
}
