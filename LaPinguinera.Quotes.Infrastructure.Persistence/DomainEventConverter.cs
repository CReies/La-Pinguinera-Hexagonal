using System.Text.Json;
using System.Text.Json.Serialization;
using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Events;

namespace Final.Project.Domain.ItemLiterature.Events;

public class DomainEventConverter : JsonConverter<DomainEvent>
{
	public override DomainEvent Read( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
	{
		using (var jsonDoc = JsonDocument.ParseValue( ref reader ))
		{
			var jsonObject = jsonDoc.RootElement;

			if (jsonObject.TryGetProperty( "Type", out var typeProperty ))
			{
				var type = typeProperty.GetString();
				var domainEventType = GetDomainEventType( type );

				if (domainEventType == null)
				{
					throw new NotSupportedException( $"Event type '{type}' is not supported." );
				}

				var domainEvent = JsonSerializer.Deserialize( jsonObject.GetRawText(), domainEventType, options );
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