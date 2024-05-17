using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Entities;
using LaPinguinera.Quotes.Domain.Model.Quote.Events;
using LaPinguinera.Quotes.Domain.Model.Quote.Factory;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Customer;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

namespace LaPinguinera.Quotes.Domain.Model.Quote;

public class QuoteBehavior : Behavior
{
	public QuoteBehavior( Quote quote )
	{
		AddCalculateIndividualSub( quote );
	}

	private void AddCalculateIndividualSub( Quote quote )
	{
		AddSub( ( IndividualPriceCalculated domainEvent ) =>
		{
			BookFactory _bookFactory = new();
			var book = _bookFactory.Create( domainEvent.Title, domainEvent.Author, domainEvent.BasePrice, domainEvent.BookType );
			book.CalculateSellPrice();

			quote.Result.Quote.Add( ([book], null, null) );
			quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );
			quote.Inventory.Add( book );
		} );
	}

	private void AddCalculateListSub( Quote quote )
	{
		AddSub
	}
}
