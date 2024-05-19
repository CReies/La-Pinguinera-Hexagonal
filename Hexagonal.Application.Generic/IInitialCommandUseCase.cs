namespace LaPinguinera.Application.Generic;

public interface IInitialCommandUseCase<T, I> where T : InitialCommand
{
	IObservable<I> Execute( IObservable<T> command );
}
