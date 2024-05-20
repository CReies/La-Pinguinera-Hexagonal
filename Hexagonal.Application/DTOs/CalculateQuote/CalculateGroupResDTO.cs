namespace LaPinguinera.Quotes.Application.DTOs.CalculateQuote;

public class CalculateGroupResDTO
{
	public List<CalculateListResDTO> Groups { get; set; }
	public decimal TotalPrice { get; set; }
	public decimal TotalBasePrice { get; set; }
	public decimal TotalDiscount { get; set; }
	public decimal TotalIncrease { get; set; }
}
