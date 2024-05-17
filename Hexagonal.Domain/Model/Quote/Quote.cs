using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Entities;
using LaPinguinera.Quotes.Domain.Model.Quote.Events;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

namespace LaPinguinera.Quotes.Domain.Model.Quote;

public class Quote : AggregateRoot<QuoteId>
{
	public List<AbstractBook> Inventory { get; set; } = [];
	public (List<(List<AbstractBook> QuoteGroup, TotalPrice? TotalPrice, Discount? Discount)> Quote, TotalPrice? TotalPrice, Discount? Discount) Result { get; set; }
	public RestBudget? RestBudget { get; set; }

	public Quote( QuoteId id ) : base( id )
	{
		Subscribe( new QuoteBehavior( this ) );
	}

	public void CalculateIndividualBook( string? title, string? author, decimal basePrice, BookType bookType )
	{
		AppendEvent( new IndividualPriceCalculated( title, author, basePrice, bookType ) ).Invoke();
	}
}
