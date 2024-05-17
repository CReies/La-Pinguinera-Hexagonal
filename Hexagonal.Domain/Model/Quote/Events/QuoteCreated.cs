using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Events.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Events;

public class QuoteCreated() : DomainEvent( EventType.QuoteCreated.ToString() )
{
}
