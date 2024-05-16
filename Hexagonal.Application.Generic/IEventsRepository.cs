namespace Hexagonal.Application.Generic;

public interface IEventsRepository
{
	DomainEvent Save( DomainEvent domainEvent );
	List<DomainEvent> FindByAggregateId( string aggregateId );
}
