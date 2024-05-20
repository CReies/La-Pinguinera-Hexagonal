using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Customer;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Shared.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Entities;

public class Customer : Entity<CustomerId>
{
	public CustomerSeniority Seniority { get; set; }
	public RegisterDate RegisterDate { get; set; }

	private Customer( CustomerId id, RegisterDate registerDate ) : base( id )
	{
		RegisterDate = registerDate;
	}

	private Customer( RegisterDate registerDate ) : this( new(), registerDate )
	{ }

	public static Customer From( CustomerId id, RegisterDate registerDate )
	{
		return new Customer( id, registerDate );
	}

	public static Customer From( RegisterDate registerDate )
	{
		return new Customer( registerDate );
	}

	public CustomerSeniority CalculateSeniority()
	{
		DateOnly today = DateOnly.FromDateTime( DateTime.Now );
		int totalDays = today.DayNumber - RegisterDate.Value.DayNumber;
		int years = totalDays / 365;

		CustomerSeniorityEnum seniority = years switch
		{
			< 1 => CustomerSeniorityEnum.LessOneYear,
			>= 1 and < 2 => CustomerSeniorityEnum.OneToTwoYears,
			>= 2 => CustomerSeniorityEnum.MoreTwoYears,
		};

		Seniority = CustomerSeniority.From( seniority );

		return Seniority;
	}
}
