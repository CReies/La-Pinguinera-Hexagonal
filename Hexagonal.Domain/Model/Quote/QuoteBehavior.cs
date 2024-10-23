using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Entities;
using LaPinguinera.Quotes.Domain.Model.Quote.Events;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;
using LaPinguinera.Quotes.Domain.Model.Quote.Shared;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Customer;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

namespace LaPinguinera.Quotes.Domain.Model.Quote;

public class QuoteBehavior : Behavior
{
	public QuoteBehavior( Quote quote )
	{
		AddQuoteCreatedSub( quote );
		AddCalculateIndividualSub( quote );
		AddCalculateListSub( quote );
		AddCalculateBudgetSub( quote );
		AddCalculateGroupSub( quote );
	}

	private void AddQuoteCreatedSub( Quote quote )
	{
		AddSub( ( DomainEvent @event ) =>
		{
			if (@event is not QuoteCreated) return;
			QuoteCreated? domainEvent = @event as QuoteCreated;
			quote.Result = new Result();
			quote.RequestedBooks = [[]];
			quote.Customer = null;
			quote.Inventory = [];
		} );
	}

	private void AddCalculateIndividualSub( Quote quote )
	{
		AddSub( ( DomainEvent @event ) =>
		{
			if (@event is not IndividualPriceCalculated) return;
			IndividualPriceCalculated domainEvent = (IndividualPriceCalculated)@event;

			AbstractBook book = quote.CreationQuoteCalculate
				.Calculate( domainEvent.BookId!, domainEvent.Title!, domainEvent.Author!, domainEvent.BasePrice, domainEvent.BookType );

			quote.Inventory.Add( book );
			domainEvent.BookId = book.Id.Value;
		} );
	}

	private void AddCalculateListSub( Quote quote )
	{
		AddSub( ( DomainEvent @event ) =>

		{
			if (@event is not ListPriceCalculated) return;
			ListPriceCalculated domainEvent = (ListPriceCalculated)@event;

			ClearResult( quote );
			quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );
			_ = quote.Customer.CalculateSeniority();

			(IResult result, List<List<AbstractBook>> requestedBook) = quote.GroupQuoteCalculate.CalculateList( domainEvent.BooksRequested, quote.Customer.Seniority.Value, quote.Inventory );

			quote.Result = result;
			quote.RequestedBooks = requestedBook;
		} );
	}

	private void AddCalculateBudgetSub( Quote quote )
	{
		AddSub( ( DomainEvent @event ) =>
		{
			if (@event is not BudgetCalculated) return;
			BudgetCalculated domainEvent = (BudgetCalculated)@event;

			ClearResult( quote );

			quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );
			_ = quote.Customer.CalculateSeniority();

			quote.BudgetQuoteCalculate.Calculate( quote.Inventory, domainEvent.BookIds, quote.Customer.Seniority.Value, domainEvent.Budget );
		} );
	}

	private void AddCalculateGroupSub( Quote quote )
	{
		AddSub( ( DomainEvent @event ) =>
		{
			if (@event is not GroupPriceCalculated) return;
			GroupPriceCalculated domainEvent = (GroupPriceCalculated)@event;

			ClearResult( quote );

			quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );
			_ = quote.Customer.CalculateSeniority();

			(IResult result, List<List<AbstractBook>> requestedBooks) = quote.GroupQuoteCalculate.CalculateGroups( domainEvent.GroupsRequested, quote.Customer.Seniority.Value, quote.Inventory );

			quote.Result = result;
			quote.RequestedBooks = requestedBooks;
		} );
	}

	private static void ClearResult( Quote quote )
	{
		quote.Result = new Result();
		quote.RequestedBooks = [[]];
		quote.Customer = null;
		quote.RestBudget = null;
	}
}
