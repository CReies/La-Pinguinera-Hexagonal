using LaPinguinera.Quotes.Domain.Generic;

namespace LaPinguinera.Quotes.Application.Generic;

public interface IInitialCommandUseCase<T, I> where T : InitialCommand
{
	IObservable<I> Execute( IObservable<T> command );
}
