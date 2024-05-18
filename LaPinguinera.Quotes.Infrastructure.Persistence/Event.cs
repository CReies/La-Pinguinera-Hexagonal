using LaPinguinera.Domain.Generic;

namespace LaPinguinera.Quotes.Infrastructure.Persistence;

public class Event : DomainEvent
{
	public Event( string type ) : base( type )
	{
	}
}
