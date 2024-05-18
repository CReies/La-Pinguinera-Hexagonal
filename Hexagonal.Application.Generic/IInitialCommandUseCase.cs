namespace LaPinguinera.Application.Generic;

public interface IInitialCommandUseCase<T> where T : InitialCommand
{
	IObservable<DomainEvent> Execute( IObservable<T> command );
}
