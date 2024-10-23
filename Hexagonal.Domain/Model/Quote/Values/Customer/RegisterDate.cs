using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Customer;

public class RegisterDate : IValueObject<DateOnly>
{
	public DateOnly Value { get; }

	private RegisterDate( DateOnly value )
	{
		Value = value;
	}

	public static RegisterDate Of( DateOnly value )
	{
		return new RegisterDate( value );
	}
}
