using LaPinguinera.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;

public class SeniorityDiscount : IValueObject<decimal>
{
	public decimal Value { get; private set; }
	private SeniorityDiscount( decimal value )
	{
		if (value < 0)
		{
			throw new ArgumentException( "Seniority discount cannot be less than zero" );
		}

		Value = value!;
	}

	public static SeniorityDiscount Of( decimal value )
	{
		return new SeniorityDiscount( value );
	}
}
