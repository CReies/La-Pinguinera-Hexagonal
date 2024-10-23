using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Customer;

public class CustomerId : Identity
{
	public CustomerId() : base() { }
	private CustomerId( string? id ) : base( id ) { }
	public static CustomerId Of( string? id ) => new( id );
}
