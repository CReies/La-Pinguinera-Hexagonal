using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Application.Generic;

public interface IEventUseCase<T> where T : DomainEvent
{
	List<DomainEvent> Execute( T domainEvent );
}
