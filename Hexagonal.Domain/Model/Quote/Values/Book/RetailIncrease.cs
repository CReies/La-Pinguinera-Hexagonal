using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;

public class RetailIncrease : IValueObject<decimal>
{
	public decimal Value { get; private set; }
	private RetailIncrease( decimal value )
	{
		if (value < 0)
		{
			throw new ArgumentException( "Retail increase cannot be less than zero" );
		}

		Value = value!;
	}

	public static RetailIncrease Of( decimal value )
	{
		return new RetailIncrease( value );
	}
}
