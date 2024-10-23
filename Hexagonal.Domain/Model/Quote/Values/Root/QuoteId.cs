using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

public class QuoteId : Identity
{
	public QuoteId() : base() { }
	private QuoteId( string? id ) : base( id ) { }
	public static QuoteId Of( string? id ) => new( id );
}