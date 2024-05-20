using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.BudgetQuoteCalculate;

public class BudgetQuoteCalculateId : Identity
{
	public BudgetQuoteCalculateId() : base() { }
	private BudgetQuoteCalculateId( string? id ) : base( id ) { }
	public static BudgetQuoteCalculateId Of( string? id ) => new( id );
}
