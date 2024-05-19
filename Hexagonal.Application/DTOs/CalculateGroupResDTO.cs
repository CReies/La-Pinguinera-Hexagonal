namespace LaPinguinera.Quotes.Application.DTOs;

public class CalculateGroupResDTO
{
	public List<CalculateListResDTO> Groups { get; set; }
	public decimal TotalPrice { get; set; }
	public decimal TotalDiscount { get; set; }
}
