namespace LaPinguinera.Application.Generic;

public interface IInitialCommandUseCase<T> where T : InitialCommand
{
	List<DomainEvent> Execute( T command );
}
