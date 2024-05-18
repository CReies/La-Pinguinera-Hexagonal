namespace LaPinguinera.Application.Generic;

public interface ICommandUseCase<T, I> where T : Command<I> where I : Identity
{
	IObservable<List<DomainEvent>> Execute( IObservable<T> command );
}
