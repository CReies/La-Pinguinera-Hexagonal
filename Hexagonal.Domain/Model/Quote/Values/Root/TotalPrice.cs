using LaPinguinera.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

public class TotalPrice : IValueObject<decimal>
{
	public decimal Value { get; private set; }

	private TotalPrice( decimal value )
	{
		if (value < 0)
		{
			throw new ArgumentException( "Rest budget cannot be less than zero" );
		}

		Value = value;
	}

	public static TotalPrice Of( decimal value )
	{
		return new TotalPrice( value );
	}
}
