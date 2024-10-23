using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;

public class FinalPrice : IValueObject<decimal>
{
	public decimal Value { get; private set; }

	private FinalPrice( decimal value )
	{
		if (value < 0)
		{
			throw new ArgumentException( "Final price cannot be less than zero" );
		}

		Value = value;
	}

	public static FinalPrice Of( decimal value )
	{
		return new FinalPrice( value );
	}
}
