using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Events.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Events;

public class BudgetCalculated( List<string> bookIds, decimal budget, DateOnly customerRegisterDate ) : DomainEvent( EventType.BudgetCalculated.ToString() )
{
	public List<string> BookIds { get; set; } = bookIds;
	public decimal Budget { get; set; } = budget;
	public DateOnly CustomerRegisterDate { get; set; } = customerRegisterDate;
}
