using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.CreationQuoteCalculate;

public class CreationQuoteCalculateId : Identity
{
	public CreationQuoteCalculateId() : base() { }
	private CreationQuoteCalculateId( string? id ) : base( id ) { }
	public static CreationQuoteCalculateId Of( string? id ) => new( id );
}
