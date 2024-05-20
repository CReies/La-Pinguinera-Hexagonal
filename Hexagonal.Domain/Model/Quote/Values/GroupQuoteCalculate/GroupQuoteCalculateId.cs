using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.GroupQuoteCalculate;

public class GroupQuoteCalculateId : Identity
{
	public GroupQuoteCalculateId() : base() { }
	private GroupQuoteCalculateId( string? id ) : base( id ) { }
	public static GroupQuoteCalculateId Of( string? id ) => new( id );
}
