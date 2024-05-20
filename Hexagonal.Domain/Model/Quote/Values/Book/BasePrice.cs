using LaPinguinera.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;

public class BasePrice : IValueObject<decimal>
{
	public decimal Value { get; private set; }

	private BasePrice( decimal value )
	{
		if (value <= 0)
		{
			throw new ArgumentException( "Base price cannot be less than or equal to zero" );
		}

		Value = value;
	}

	public static BasePrice Of( decimal value )
	{
		return new BasePrice( value );
	}
}