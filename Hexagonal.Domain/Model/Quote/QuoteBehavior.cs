using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Entities;
using LaPinguinera.Quotes.Domain.Model.Quote.Events;
using LaPinguinera.Quotes.Domain.Model.Quote.Factory;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;
using LaPinguinera.Quotes.Domain.Model.Quote.Shared;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;
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

			ClearResult( quote );

			AbstractBook book = quote.CreationQuoteCalculate
				.Calculate( domainEvent.BookId!, domainEvent.Title!, domainEvent.Author!, domainEvent.BasePrice, domainEvent.BookType );

			quote.Result.Quotes[0].Books.Add( book );

			//quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );
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

			var (result, requestedBook) = quote.GroupQuoteCalculate.CalculateList( domainEvent.BooksRequested, quote.Customer.Seniority.Value, quote.Inventory );

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

			var (result, requestedBooks, restBudget) = quote.BudgetQuoteCalculate.Calculate( quote.Inventory, domainEvent.BookIds, quote.Customer.Seniority.Value, domainEvent.Budget );

			quote.Result = result;
			quote.RequestedBooks = requestedBooks;
			quote.RestBudget = RestBudget.Of( restBudget );
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

			var (result, requestedBooks) = quote.GroupQuoteCalculate.CalculateGroups( domainEvent.GroupsRequested, quote.Customer.Seniority.Value, quote.Inventory );

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
