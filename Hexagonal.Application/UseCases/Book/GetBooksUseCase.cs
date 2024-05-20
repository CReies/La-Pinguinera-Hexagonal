using LaPinguinera.Quotes.Application.DTOs.Book;
using LaPinguinera.Quotes.Application.Generic;
using LaPinguinera.Quotes.Application.Mappers.Book;
using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace LaPinguinera.Quotes.Application.UseCases.Book;

public class GetBooksUseCase( IEventsRepository eventsRepository ) : ICommandUseCase<GetBooksCommand, QuoteId, GetBooksResDTO>
{
	private readonly IEventsRepository _repository = eventsRepository;

	public IObservable<GetBooksResDTO> Execute( IObservable<GetBooksCommand> commandObservable )
	{
		return commandObservable.SelectMany( command =>

		_repository.FindByAggregateId( command.AggregateId.Value )
				.ToObservable()
				.SelectMany(
				events =>
				{
					Domain.Model.Quote.Quote quote = Domain.Model.Quote.Quote.From( command.AggregateId.Value, events );

					List<DomainEvent> domainEvents = quote.GetUncommittedChanges().ToList();
					GetBooksResMapper mapper = new();

					return domainEvents.ToObservable()
									.SelectMany( domainEvent => _repository.Save( domainEvent ).ToObservable() )
									.ToList()
									.Do( _ => quote.MarkAsCommitted() )
									.Select( _ => mapper.Map( quote.Inventory ) );
				} )
		);
	}
}
