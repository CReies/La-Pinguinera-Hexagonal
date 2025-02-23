﻿using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Commands;

public class CalculateGroupCommand(
	string quoteId,
	List<List<(string bookId, int bookQuantity)>> bookGroups,
	DateOnly customerRegisterDate
) : Command<QuoteId>( QuoteId.Of( quoteId ) )
{
	public List<List<(string bookId, int bookQuantity)>> BookGroups { get; } = bookGroups;
	public DateOnly CustomerRegisterDate { get; } = customerRegisterDate;
}
