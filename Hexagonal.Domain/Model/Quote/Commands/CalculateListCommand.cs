using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Commands;

public class CalculateListCommand(
	string quoteId,
	List<(string bookId, int bookQuantity)> books,
	DateOnly customerRegisterDate
) : Command<QuoteId>( QuoteId.Of( quoteId ) )
{
	public List<(string bookId, int bookQuantity)> Books { get; } = books;
	public DateOnly CustomerRegisterDate { get; } = customerRegisterDate;
}
