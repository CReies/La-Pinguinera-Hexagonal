using LaPinguinera.Quotes.Application.DTOs.Quote;
using LaPinguinera.Quotes.Application.Generic;
using LaPinguinera.Quotes.Application.Mappers.Quote;
using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;
using System.Reactive.Linq;

namespace LaPinguinera.Quotes.Application.UseCases.Quote;

public class CreateQuoteUseCase( IEventsRepository eventsRepository ) : IInitialCommandUseCase<CreateQuoteCommand, CreateQuoteResDTO>
{
	private readonly IEventsRepository _eventsRepository = eventsRepository;

	public IObservable<CreateQuoteResDTO> Execute( IObservable<CreateQuoteCommand> command )
	{
		Quote quote = new();
		IObservable<DomainEvent> domainEvents = quote.GetUncommittedChanges().ToObservable();

		CreateQuoteResMapper mapper = new();

		_ = domainEvents.Subscribe( async domainEvent =>
		{
			_ = await _eventsRepository.Save( domainEvent );
		} );

		quote.MarkAsCommitted();

		return domainEvents.Select( mapper.Map );
	}
}
