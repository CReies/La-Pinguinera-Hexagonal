using System.Reactive.Linq;
using LaPinguinera.Application.Generic;
using LaPinguinera.Quotes.Application.DTOs;
using LaPinguinera.Quotes.Application.Mappers;
using LaPinguinera.Quotes.Domain.Model.Quote;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;

namespace LaPinguinera.Quotes.Application.UseCases;

public class CreateQuoteUseCase( IEventsRepository eventsRepository ) : IInitialCommandUseCase<CreateQuoteCommand, CreateQuoteResDTO>
{
	private readonly IEventsRepository _eventsRepository = eventsRepository;

	public IObservable<CreateQuoteResDTO> Execute( IObservable<CreateQuoteCommand> command )
	{
		var quote = new Quote();
		var domainEvents = quote.GetUncommittedChanges().ToObservable();

		CreateQuoteResMapper mapper = new();

		domainEvents.Subscribe( async domainEvent =>
		{
			await _eventsRepository.Save( domainEvent );
		} );

		quote.MarkAsCommitted();

		return domainEvents.Select( _ => mapper.Map( quote.Id.Value ) );
	}
}
