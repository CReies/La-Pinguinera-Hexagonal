using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Shared.Helper;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.BudgetQuoteCalculate;

public class Result : IValueObject<(
	List<BookResult> Books,
	decimal TotalPrice,
	decimal TotalBasePrice,
	decimal TotalDiscount,
	decimal TotalIncrease,
	decimal RestOfBudget,
	int TotalBooks,
	int TotalNovels
)>
{
	public (
		List<BookResult> Books,
		decimal TotalPrice,
		decimal TotalBasePrice,
		decimal TotalDiscount,
		decimal TotalIncrease,
		decimal RestOfBudget,
		int TotalBooks,
		int TotalNovels
	) Value
	{ get; private set; }

	private Result(
		List<BookResult> books,
		decimal totalPrice,
		decimal totalBasePrice,
		decimal totalDiscount,
		decimal totalIncrease,
		decimal restOfBudget,
		int totalBooks,
		int totalNovels )
	{
		if (books.Count == 0)
		{
			throw new ArgumentException( "Books cannot be empty" );
		}

		if (totalPrice < 0)
		{
			throw new ArgumentException( "Total price cannot be less than 0" );
		}

		if (totalBasePrice < 0)
		{
			throw new ArgumentException( "Total base price cannot be less than 0" );
		}

		if (totalDiscount < 0)
		{
			throw new ArgumentException( "Total discount cannot be less than 0" );
		}

		if (totalIncrease < 0)
		{
			throw new ArgumentException( "Total increase cannot be less than 0" );
		}

		if (restOfBudget < 0)
		{
			throw new ArgumentException( "Rest of budget cannot be less than 0" );
		}

		if (totalBooks < 1)
		{
			throw new ArgumentException( "Total books cannot be less than 1" );
		}

		if (totalNovels < 1)
		{
			throw new ArgumentException( "Total novels cannot be less than 1" );
		}

		Value = (books, totalPrice, totalBasePrice, totalDiscount, totalIncrease, restOfBudget, totalBooks, totalNovels);
	}

	public static Result Of(
		List<BookResult> books,
		decimal totalPrice,
		decimal totalBasePrice,
		decimal totalDiscount,
		decimal totalIncrease,
		decimal restOfBudget,
		int totalBooks,
		int totalNovels )
	{
		return new Result( books, totalPrice, totalBasePrice, totalDiscount, totalIncrease, restOfBudget, totalBooks, totalNovels );
	}
}
