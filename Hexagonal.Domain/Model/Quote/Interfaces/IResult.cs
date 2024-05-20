namespace LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;

public interface IResult
{
	List<IGroupQuote> Quotes { get; set; }
	decimal TotalPrice { get; set; }
	decimal TotalBasePrice { get; set; }
	decimal TotalDiscount { get; set; }
	decimal TotalIncrease { get; set; }
}
