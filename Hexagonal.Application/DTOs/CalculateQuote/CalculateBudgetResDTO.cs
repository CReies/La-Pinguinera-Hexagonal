namespace LaPinguinera.Quotes.Application.DTOs.CalculateQuote;

public class CalculateBudgetResDTO
{
	public List<CalculateIndividualResDTO> Books { get; set; }
	public decimal TotalPrice { get; set; }
	public decimal TotalDiscount { get; set; }
	public decimal RestOfBudget { get; set; }
}
