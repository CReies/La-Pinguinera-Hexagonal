using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Events.Enums;
using System.Text.Json.Serialization;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Events;

public class BudgetCalculated : DomainEvent
{

	public List<string> BookIds { get; set; }
	public decimal Budget { get; set; }
	public DateOnly CustomerRegisterDate { get; set; }

	public BudgetCalculated( List<string> bookIds, decimal budget, DateOnly customerRegisterDate ) : base( EventType.BudgetCalculated.ToString() )
	{
		BookIds = bookIds;
		Budget = budget;
		CustomerRegisterDate = customerRegisterDate;
	}

	[JsonConstructor]
	public BudgetCalculated( DateTime moment, int version, string uuid, string type, string aggregateName, string aggregateId, List<string> bookIds, decimal budget, DateOnly customerRegisterDate ) : base( moment, version, uuid, type, aggregateName, aggregateId )
	{
		BookIds = bookIds;
		Budget = budget;
		CustomerRegisterDate = customerRegisterDate;
	}
}
