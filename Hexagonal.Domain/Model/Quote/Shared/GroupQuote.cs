using LaPinguinera.Quotes.Domain.Model.Quote.Entities;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Shared;

public class GroupQuote() : IGroupQuote
{
	public List<AbstractBook> Books { get; set; } = [];
	public decimal TotalPrice { get; set; } = 0;
	public decimal TotalDiscount { get; set; } = 0;
}
