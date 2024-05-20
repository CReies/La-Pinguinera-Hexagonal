using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LaPinguinera.Quotes.Infrastructure.Persistence;

public class DomainEventConverter : JsonConverter<DomainEvent>
{
	public override DomainEvent Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
	{
		using (JsonDocument jsonDoc = JsonDocument.ParseValue( ref reader ))
		{
			JsonElement jsonObject = jsonDoc.RootElement;

			if (jsonObject.TryGetProperty( "Type", out JsonElement typeProperty ))
			{
				string? type = typeProperty.GetString();
				Type? domainEventType = GetDomainEventType( type );

				if (domainEventType == null)
				{
					throw new NotSupportedException( $"Event type '{type}' is not supported." );
				}

				object? domainEvent = JsonSerializer.Deserialize( jsonObject.GetRawText(), domainEventType, options );
				return (DomainEvent)domainEvent!;
			}
			else
			{
				throw new JsonException( "Type property is missing in the JSON." );
			}
		}
	}

	public override void Write( Utf8JsonWriter writer, DomainEvent value, JsonSerializerOptions options )
	{
		JsonSerializer.Serialize( writer, value, value.GetType(), options );
	}

	private Type? GetDomainEventType( string type )
	{
		return type switch
		{
			"QuoteCreated" => typeof( QuoteCreated ),
			"IndividualPriceCalculated" => typeof( IndividualPriceCalculated ),
			"ListPriceCalculated" => typeof( ListPriceCalculated ),
			"BudgetCalculated" => typeof( BudgetCalculated ),
			"GroupPriceCalculated" => typeof( GroupPriceCalculated ),
			_ => null
		};
	}
}