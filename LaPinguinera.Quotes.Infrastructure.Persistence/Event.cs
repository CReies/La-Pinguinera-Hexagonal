using LaPinguinera.Domain.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json;

namespace LaPinguinera.Infrastructure.Persistence;

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
		var options = new JsonSerializerOptions() { IncludeFields = true };
		options.Converters.Add( new DomainEventConverter() );
		return JsonSerializer.Serialize( domainEvent, options );
	}

	public static DomainEvent DeserializeEvent( string json )
	{
		var options = new JsonSerializerOptions() { IncludeFields = true };
		options.Converters.Add( new DomainEventConverter() );
		return JsonSerializer.Deserialize<DomainEvent>( json, options )!;
	}
}
