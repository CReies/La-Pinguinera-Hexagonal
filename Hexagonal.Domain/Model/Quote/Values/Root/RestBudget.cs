using LaPinguinera.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

public class RestBudget : IValueObject<decimal>
{
	public decimal Value { get; private set; }

	private RestBudget( decimal value )
	{
		if (value < 0)
		{
			throw new ArgumentException( "Rest budget cannot be less than zero" );
		}

		Value = value;
	}

	public static RestBudget Of( decimal value )
	{
		return new RestBudget( value );
	}
}
