using LaPinguinera.Quotes.Domain.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;

namespace LaPinguinera.Quotes.Infrastructure.Persistence;

public class Event
{
	[BsonId]
	[BsonRepresentation( BsonType.ObjectId )]
	public string _id { get; set; }
	public string AggregateId { get; set; }
	public string Type { get; set; }
	public DateTime Moment { get; set; }
	public string EventBody { get; set; }


	public static string WrapEvent( DomainEvent domainEvent )
	{
		JsonSerializerOptions options = new() { IncludeFields = true };
		options.Converters.Add( new DomainEventConverter() );
		return JsonSerializer.Serialize( domainEvent, options );
	}

	public static DomainEvent DeserializeEvent( string json )
	{
		JsonSerializerOptions options = new() { IncludeFields = true };
		options.Converters.Add( new DomainEventConverter() );
		return JsonSerializer.Deserialize<DomainEvent>( json, options )!;
	}
}
