﻿using LaPinguinera.Application.Generic;
using LaPinguinera.Quotes.Application.DTOs;
using LaPinguinera.Quotes.Application.Mappers;
using LaPinguinera.Quotes.Domain.Model.Quote;
using LaPinguinera.Quotes.Domain.Model.Quote.Commands;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace LaPinguinera.Quotes.Application.UseCases;

public class CalculateGroupUseCase( IEventsRepository repository )
	: ICommandUseCase<CalculateGroupCommand, QuoteId, CalculateGroupResDTO>
{
	private readonly IEventsRepository _repository = repository;

	public IObservable<CalculateGroupResDTO> Execute( IObservable<CalculateGroupCommand> commandObservable )
	{
		return commandObservable.SelectMany( command =>

		_repository.FindByAggregateId( command.AggregateId.Value )
			.ToObservable()
			.SelectMany(
			events =>
			{
				var quote = Quote.From( command.AggregateId.Value, events );
				quote.CalculateGroup( command.BookGroups, command.CustomerRegisterDate );

				var domainEvents = quote.GetUncommittedChanges().ToList();
				CalculateGroupResMapper mapper = new();

				return domainEvents.ToObservable()
					.SelectMany( domainEvent => _repository.Save( domainEvent ).ToObservable() )
					.ToList()
					.Do( _ => quote.MarkAsCommitted() )
					.Select( /* TODO: map the response */ _ => mapper.Map( quote.Result ) );
			} )
		);
	}
}
