namespace LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

public interface IResult
{
	List<IGroupQuote> Quotes { get; set; }
	decimal TotalPrice { get; set; }
	decimal TotalDiscount { get; set; }
}
