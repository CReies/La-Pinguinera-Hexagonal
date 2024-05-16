namespace Hexagonal.Application.Generic;

public interface IEventUseCase<T> where T : DomainEvent
{
	List<DomainEvent> Execute( T domainEvent );
}
