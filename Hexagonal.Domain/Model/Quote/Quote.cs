using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Entities;
using LaPinguinera.Quotes.Domain.Model.Quote.Events;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

namespace LaPinguinera.Quotes.Domain.Model.Quote;

public class Quote : AggregateRoot<QuoteId>
{
	public List<AbstractBook> Inventory { get; set; }
	public (List<(List<AbstractBook> QuoteGroup, TotalPrice? TotalPrice, Discount? Discount)> Quote, TotalPrice? TotalPrice, Discount? Discount) Result { get; set; }
	public List<List<AbstractBook>> RequestedBooks { get; set; }
	public Customer? Customer { get; set; }
	public RestBudget? RestBudget { get; set; }

	public Quote( QuoteId id ) : base( id )
	{
	}

	public Quote() : base( new QuoteId() )
	{
		Subscribe( new QuoteBehavior( this ) );
		AppendEvent( new QuoteCreated() ).Invoke();
	}

	public void CalculateIndividual( string? title, string? author, decimal basePrice, BookType bookType, DateOnly registerDate )
	{
		AppendEvent( new IndividualPriceCalculated( title, author, basePrice, bookType, registerDate ) ).Invoke();
	}

	public void CalculateList( List<(string bookId, int quantity)> booksRequested, DateOnly customerRegisterDate )
	{
		AppendEvent( new ListPriceCalculated( booksRequested, customerRegisterDate ) ).Invoke();
	}
}
