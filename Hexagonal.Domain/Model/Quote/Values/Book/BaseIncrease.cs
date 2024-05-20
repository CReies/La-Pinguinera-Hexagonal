using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;

public class BaseIncrease : IValueObject<decimal>
{
	public decimal Value { get; private set; }

	private BaseIncrease( decimal value )
	{
		if (value < 0)
		{
			throw new ArgumentException( "Base increase cannot be less than zero" );
		}

		Value = value;
	}

	public static BaseIncrease Of( decimal value )
	{
		return new BaseIncrease( value );
	}
}
