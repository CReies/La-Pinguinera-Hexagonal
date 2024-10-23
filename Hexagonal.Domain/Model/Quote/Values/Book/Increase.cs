using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;

public class Increase : IValueObject<decimal>
{
	public decimal Value { get; }

	private Increase( decimal value )
	{
		Value = value;
	}

	public static Increase Of( decimal value )
	{
		return new( value );
	}
}
