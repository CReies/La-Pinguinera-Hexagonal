using LaPinguinera.Application.Generic;
using LaPinguinera.Domain.Generic;
using LaPinguinera.Infrastructure.Persistence;
using MongoDB.Driver;

namespace Hexagonal.Library.Quotes.Infrastructure.Persistence
{
	public class EventsRepository : IEventsRepository
	{
		private readonly IMongoCollection<Event> _eventRepository;

		public EventsRepository( IMongoCollection<Event> repository )
		{
			_eventRepository = repository;
		}

		public async Task<List<DomainEvent>> FindByAggregateId( string aggregateId )
		{
			var cursor = await _eventRepository.FindAsync( ( eve )
					=> eve.AggregateId.Equals( aggregateId ) );
			var events = await cursor.ToListAsync();
			return events.Select( e => Event.DeserializeEvent( e.EventBody ) ).ToList();
		}

		public async Task<List<DomainEvent>> Save( DomainEvent domainEvent )
		{
			var @event = new Event()
			{
				AggregateId = domainEvent.AggregateId,
				Type = domainEvent.Type,
				Moment = domainEvent.Moment,
				EventBody = Event.WrapEvent( domainEvent )
			};

			await _eventRepository.InsertOneAsync( @event );
			return await FindByAggregateId( domainEvent.AggregateId );
		}
	}
}