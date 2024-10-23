using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Events.Enums;
using System.Text.Json.Serialization;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Events;

public class QuoteCreated : DomainEvent
{
	public QuoteCreated() : base( EventType.QuoteCreated.ToString() )
	{ }

	[JsonConstructor]
	public QuoteCreated( DateTime moment, int version, string uuid, string type, string aggregateName, string aggregateId ) : base( moment, version, uuid, type, aggregateName, aggregateId )
	{ }
}
