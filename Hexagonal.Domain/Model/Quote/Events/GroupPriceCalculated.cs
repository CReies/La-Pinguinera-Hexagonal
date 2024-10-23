using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Events.Enums;
using System.Text.Json.Serialization;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Events;

public class GroupPriceCalculated : DomainEvent
{
	public List<List<(string bookId, int quantity)>> GroupsRequested { get; set; }
	public DateOnly CustomerRegisterDate { get; set; }

	public GroupPriceCalculated( List<List<(string bookId, int quantity)>> groupsRequested, DateOnly customerRegisterDate ) : base( EventType.GroupPriceCalculated.ToString() )
	{
		GroupsRequested = groupsRequested;
		CustomerRegisterDate = customerRegisterDate;
	}

	[JsonConstructor]
	public GroupPriceCalculated( DateTime moment, int version, string uuid, string type, string aggregateName, string aggregateId, List<List<(string bookId, int quantity)>> groupsRequested, DateOnly customerRegisterDate ) : base( moment, version, uuid, type, aggregateName, aggregateId )
	{
		GroupsRequested = groupsRequested;
		CustomerRegisterDate = customerRegisterDate;
	}
}
