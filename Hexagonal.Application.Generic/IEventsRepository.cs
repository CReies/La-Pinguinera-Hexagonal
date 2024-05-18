namespace LaPinguinera.Application.Generic;

public interface IEventsRepository
{
	Task<DomainEvent> Save( DomainEvent domainEvent );
	Task<List<DomainEvent>> FindByAggregateId( string aggregateId );
}
