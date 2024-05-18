using LaPinguinera.Application.Generic;
using LaPinguinera.Domain.Generic;
using MongoDB.Driver;

namespace LaPinguinera.Quotes.Infrastructure.Persistence;

public class EventsRepository : IEventsRepository
{
	private readonly IMongoCollection<DomainEvent> _eventRepository;

	public EventsRepository( IMongoCollection<DomainEvent> repository )
	{
		_eventRepository = repository;
	}

	public async Task<List<DomainEvent>> FindByAggregateId( string aggregateId )
	{
		var filter = Builders<DomainEvent>.Filter.Eq( e => e.AggregateId, aggregateId );
		var cursor = await _eventRepository.FindAsync( filter );
		return cursor.ToList();
	}

	public async Task<List<DomainEvent>> Save( DomainEvent domainEvent )
	{
		await _eventRepository.InsertOneAsync( domainEvent );

		return await FindByAggregateId( domainEvent.AggregateId );
	}
}
