using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Shared.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Entities;

public class AbstractBook : Entity<BookId>
{
	public Data Data { get; set; }
	public PriceModifiers PriceModifiers { get; set; }
	public BaseIncrease BaseIncrease { get; set; }
	public BasePrice BasePrice { get; set; }
	public FinalPrice FinalPrice { get; set; }

	public AbstractBook( BookId id, Data data, PriceModifiers priceModifiers, BaseIncrease baseIncrease, BasePrice basePrice, FinalPrice finalPrice ) : base( id )
	{
		Data = data;
		PriceModifiers = priceModifiers;
		BaseIncrease = baseIncrease;
		BasePrice = basePrice;
		FinalPrice = finalPrice;
	}

	public AbstractBook( Data data, PriceModifiers priceModifiers, BaseIncrease baseIncrease, BasePrice basePrice, FinalPrice finalPrice ) :
		this( new(), data, priceModifiers, baseIncrease, basePrice, finalPrice )
	{ }

	/*	
	 *	En los child
	 *	
	 *	public AbstractBook From( string? title, string author, decimal basePrice, BookType type ) => new
			(
				Data.Of( title, author, 0, type ),
				PriceModifiers.Of( 0, 0, 0 ),
				BaseIncrease.Of( 0 ),
				BasePrice.Of( basePrice ),
				FinalPrice.Of( 0 )
			);

		public AbstractBook From( string? id, string? title, string author, decimal basePrice, BookType type ) => new
			(
				BookId.Of( id ),
				Data.Of( title, author, 0, type ),
				PriceModifiers.Of( 0, 0, 0 ),
				BaseIncrease.Of( 0 ),
				BasePrice.Of( basePrice ),
				FinalPrice.Of( 0 )
			);*/

	public void CalculateSellPrice()
	{
		var sellPrice = BasePrice.Value * (1 + BaseIncrease.Value);
		Data = Data.Of( Data.Value.Title, Data.Value.Author, sellPrice, Data.Value.Type );
	}

	public void ApplyDiscount( CustomerSeniority customerSeniority )
	{
		var seniorityDiscounts = new Dictionary<CustomerSeniority, decimal>
		{
			{ CustomerSeniority.LessOneYear, 0 },
			{ CustomerSeniority.OneToTwoYears, 0.12m },
			{ CustomerSeniority.MoreTwoYears, 0.17m }
		};

		var seniorDiscount = seniorityDiscounts[customerSeniority];
		var finalPrice = BasePrice.Value * (1 + PriceModifiers.Value.RetailIncrease) * (1 - PriceModifiers.Value.WholeSaleDiscount) * (1 - seniorDiscount);

		FinalPrice = FinalPrice.Of( finalPrice );
	}
}
