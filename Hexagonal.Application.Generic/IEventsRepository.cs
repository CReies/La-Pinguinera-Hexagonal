namespace LaPinguinera.Application.Generic;

public interface IEventsRepository
{
	Task<List<DomainEvent>> Save( DomainEvent domainEvent );
	Task<List<DomainEvent>> FindByAggregateId( string aggregateId );
}
