using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;
using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Commands;

public class CalculateIndividualCommand(
	string quoteId,
	string title,
	string author,
	decimal price,
	BookType type
) : Command<QuoteId>( QuoteId.Of( quoteId ) )
{
	public string Title { get; } = title;
	public string Author { get; } = author;
	public decimal Price { get; } = price;
	public BookType Type { get; } = type;
}
