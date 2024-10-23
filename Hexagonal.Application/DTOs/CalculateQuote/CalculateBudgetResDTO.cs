namespace LaPinguinera.Quotes.Application.DTOs.CalculateQuote;

public class CalculateBudgetResDTO
{
	public List<CalculateIndividualResDTO> Books { get; set; }
	public decimal TotalPrice { get; set; }
	public decimal TotalBasePrice { get; set; }
	public decimal TotalDiscount { get; set; }
	public decimal TotalIncrease { get; set; }
	public decimal RestOfBudget { get; set; }
	public int TotalBooks { get; set; }
	public int TotalNovels { get; set; }
}
