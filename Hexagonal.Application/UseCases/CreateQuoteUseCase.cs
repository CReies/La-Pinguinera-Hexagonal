﻿using System.Reactive.Linq;
using LaPinguinera.Application.Generic;
using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;

namespace LaPinguinera.Quotes.Application.UseCases;

public class CreateQuoteUseCase( IEventsRepository eventsRepository ) : IInitialCommandUseCase<CreateQuoteCommand>
{
	private readonly IEventsRepository _eventsRepository = eventsRepository;

	public IObservable<DomainEvent> Execute( IObservable<CreateQuoteCommand> command )
	{
		var quote = new Quote();
		var domainEvents = quote.GetUncommittedChanges().ToObservable();

		domainEvents.Subscribe( async domainEvent =>
		{
			await _eventsRepository.Save( domainEvent );
		} );

		quote.MarkAsCommitted();
		return domainEvents;
	}
}