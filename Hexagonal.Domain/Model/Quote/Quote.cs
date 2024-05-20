using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Entities;
using LaPinguinera.Quotes.Domain.Model.Quote.Events;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

namespace LaPinguinera.Quotes.Domain.Model.Quote;

public class Quote : AggregateRoot<QuoteId>
{
	public List<AbstractBook> Inventory { get; set; }
	public IResult Result { get; set; }
	public List<List<AbstractBook>> RequestedBooks { get; set; }
	public Customer? Customer { get; set; }
	public RestBudget? RestBudget { get; set; }

	public Quote( QuoteId id ) : base( id )
	{
		Subscribe( new QuoteBehavior( this ) );
	}

	public Quote() : base( new QuoteId() )
	{
		Subscribe( new QuoteBehavior( this ) );
		AppendEvent( new QuoteCreated() ).Invoke();
	}

	public static Quote From( string quoteId, List<DomainEvent> events )
	{
		Quote quote = new( QuoteId.Of( quoteId ) );
		events.ForEach( quote.Apply );

		return quote;
	}

	public void CalculateIndividual( string? title, string? author, decimal basePrice, BookType bookType )
	{
		AppendEvent( new IndividualPriceCalculated( title, author, basePrice, bookType ) ).Invoke();
	}

	public void CalculateList( List<(string bookId, int quantity)> booksRequested, DateOnly customerRegisterDate )
	{
		AppendEvent( new ListPriceCalculated( booksRequested, customerRegisterDate ) ).Invoke();
	}

	public void CalculateBudget( List<string> bookIds, decimal budget, DateOnly customerRegisterDate )
	{
		AppendEvent( new BudgetCalculated( bookIds, budget, customerRegisterDate ) ).Invoke();
	}

	public void CalculateGroup( List<List<(string bookId, int quantity)>> groupsRequested, DateOnly customerRegisterDate )
	{
		AppendEvent( new GroupPriceCalculated( groupsRequested, customerRegisterDate ) ).Invoke();
	}
}
