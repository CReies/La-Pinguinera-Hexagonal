using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

public class Discount : IValueObject<decimal>
{
	public decimal Value { get; private set; }

	private Discount( decimal value )
	{
		if (value < 0)
		{
			throw new ArgumentException( "Discount cannot be less than zero" );
		}

		Value = value;
	}

	public static Discount Of( decimal value )
	{
		return new Discount( value );
	}
}
