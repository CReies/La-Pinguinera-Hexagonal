using LaPinguinera.Quotes.Domain.Model.Quote.Values.Shared.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Customer;

public class CustomerSeniority
{
	public CustomerSeniorityEnum Value { get; }

	private CustomerSeniority( CustomerSeniorityEnum value )
	{
		Value = value;
	}

	public static CustomerSeniority From( CustomerSeniorityEnum value )
	{
		return new CustomerSeniority( value );
	}
}
