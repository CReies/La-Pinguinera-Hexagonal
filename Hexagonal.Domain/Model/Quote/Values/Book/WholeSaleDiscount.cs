using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;

public class WholeSaleDiscount : IValueObject<decimal>
{
	public decimal Value { get; private set; }
	private WholeSaleDiscount( decimal value )
	{
		if (value < 0)
		{
			throw new ArgumentException( "Whole sale discount cannot be less than zero" );
		}

		Value = value!;
	}

	public static WholeSaleDiscount Of( decimal value )
	{
		return new WholeSaleDiscount( value );
	}
}
