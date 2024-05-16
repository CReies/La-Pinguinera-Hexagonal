namespace Hexagonal.Application.Generic;

public interface ICommandUseCase<T, I> where T : Command<I> where I : Identity
{
	List<DomainEvent> Execute( T command );
}
