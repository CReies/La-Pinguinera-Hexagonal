using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Events.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Events;

public class GroupPriceCalculated( List<List<(string bookId, int quantity)>> groupsRequested, DateOnly customerRegisterDate ) : DomainEvent( EventType.GroupPriceCalculated.ToString() )
{
	public List<List<(string bookId, int quantity)>> GroupsRequested { get; set; } = groupsRequested;
	public DateOnly CustomerRegisterDate { get; set; } = customerRegisterDate;
}
