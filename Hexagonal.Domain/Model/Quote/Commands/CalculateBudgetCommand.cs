using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Commands;

public class CalculateBudgetCommand(
	string quoteId,
	List<string> bookIds,
	decimal budget,
	DateOnly customerRegisterDate
) : Command<QuoteId>( QuoteId.Of( quoteId ) )
{
	public List<string> BookIds { get; } = bookIds;
	public decimal Budget { get; private set; } = budget;
	public DateOnly CustomerRegisterDate { get; } = customerRegisterDate;
}
