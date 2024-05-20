using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Application.Generic;

public interface IEventsRepository
{
	Task<List<DomainEvent>> Save( DomainEvent domainEvent );
	Task<List<DomainEvent>> FindByAggregateId( string aggregateId );
}
