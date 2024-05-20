using LaPinguinera.Quotes.Application.Generic;
using LaPinguinera.Quotes.Domain.Generic;
using MongoDB.Driver;

namespace LaPinguinera.Quotes.Infrastructure.Persistence
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
			IAsyncCursor<Event> cursor = await _eventRepository.FindAsync( ( eve )
					=> eve.AggregateId.Equals( aggregateId ) );
			List<Event> events = await cursor.ToListAsync();
			return events.Select( e => Event.DeserializeEvent( e.EventBody ) ).ToList();
		}

		public async Task<List<DomainEvent>> Save( DomainEvent domainEvent )
		{
			Event @event = new()
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