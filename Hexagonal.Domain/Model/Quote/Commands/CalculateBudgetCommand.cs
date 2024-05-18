using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Commands;

public class CalculateBudgetCommand(
	string quoteId,
	List<string> bookIds,
	DateOnly customerRegisterDate
) : Command<QuoteId>( QuoteId.Of( quoteId ) )
{
	public List<string> BookIds { get; } = bookIds;
	public DateOnly CustomerRegisterDate { get; } = customerRegisterDate;
}
