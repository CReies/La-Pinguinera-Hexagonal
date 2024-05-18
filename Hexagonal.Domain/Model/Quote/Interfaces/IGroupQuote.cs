using LaPinguinera.Quotes.Domain.Model.Quote.Entities;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

public interface IGroupQuote
{
	List<AbstractBook> Books { get; set; }
	decimal TotalPrice { get; set; }
	decimal TotalDiscount { get; set; }
}
