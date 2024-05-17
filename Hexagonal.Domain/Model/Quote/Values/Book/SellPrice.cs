using LaPinguinera.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;

public class SellPrice: IValueObject<decimal>
{
	public decimal Value { get; private set; }
	private SellPrice( decimal value )
	{
		if (value < 0)
		{
			throw new ArgumentException( "Sell price cannot be less than zero" );
		}

		Value = value!;
	}

	public static SellPrice Of( decimal value )
	{
		return new SellPrice( value );
	}
}
