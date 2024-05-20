using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;
using LaPinguinera.Quotes.Domain.Model.Quote.Shared;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.BudgetQuoteCalculate;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Shared.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Entities;

public class BudgetQuoteCalculate : Entity<BudgetQuoteCalculateId>
{
	private BudgetQuoteCalculate( BudgetQuoteCalculateId id ) : base( id )
	{ }

	public BudgetQuoteCalculate() : this( new() )
	{ }

	public (IResult result, List<List<AbstractBook>> requestedBooks, decimal restOfBudget) Calculate( List<AbstractBook> inventory, List<string> requestedBookIds, CustomerSeniorityEnum seniority, decimal budget )
	{
		List<List<AbstractBook>> requestedBooks = [[]];
		AbstractBook cheapBookFromInventory = inventory
			.Where( book =>
			book.Data.Value.Type == BookType.BOOK && requestedBookIds.Contains( book.Id.Value ) )
			.OrderBy( book => book.SellPrice.Value )
			.FirstOrDefault() ?? throw new KeyNotFoundException( "You need to request at least one book" );

		AbstractBook cheapNovelFromInventory = inventory
			.Where( novel => novel.Data.Value.Type == BookType.NOVEL && requestedBookIds.Contains( novel.Id.Value ) )
			.OrderBy( novel => novel.SellPrice.Value )
			.FirstOrDefault() ?? throw new KeyNotFoundException( "You need to request at least one novel" );

		AbstractBook cheapBook = cheapBookFromInventory.Clone();
		AbstractBook cheapNovel = cheapNovelFromInventory.Clone();

		requestedBooks.Add( [cheapBook, cheapNovel] );

		AbstractBook expensiveBook = cheapBook.SellPrice.Value > cheapNovel.SellPrice.Value ? cheapBook : cheapNovel;
		AbstractBook cheapestBook = cheapBook.SellPrice.Value < cheapNovel.SellPrice.Value ? cheapBook : cheapNovel;

		(IResult result, decimal restOfBudget) = SetMaxBooksAndRemainingBudget( cheapBook.Clone(), expensiveBook.Clone(), seniority, budget );

		return (result, requestedBooks, restOfBudget);
	}

	private (IResult result, decimal restOfBudget) SetMaxBooksAndRemainingBudget( AbstractBook cheapBook, AbstractBook expensiveBook, CustomerSeniorityEnum seniority, decimal totalBudget )
	{
		decimal budget = totalBudget - expensiveBook.SellPrice.Value;
		IResult result = new Result();

		expensiveBook.ApplyDiscount( seniority );
		result.Quotes[0].Books.Add( expensiveBook.Clone() );

		decimal restOfBudget = budget;

		int quantity;

		for (quantity = 0; quantity < 600; quantity++)
		{
			AbstractBook bookEntity = GetBookEntity( cheapBook.Clone(), quantity, seniority );

			if (restOfBudget < bookEntity.FinalPrice!.Value) break;

			restOfBudget -= bookEntity.FinalPrice.Value;
			result.Quotes[0].Books.Add( bookEntity.Clone() );
		}

		if (quantity <= 10) throw new ArgumentException( "You don't have enough budget for a major sale" );

		result.Quotes[0].TotalPrice = totalBudget - restOfBudget;

		decimal totalBasePrice = result.Quotes[0].Books.Sum( book => book.SellPrice!.Value );
		decimal totalDiscount = result.Quotes[0].Books.Sum( book => book.Discount!.Value );
		decimal totalIncrease = result.Quotes[0].Books.Sum( book => book.Increase!.Value );

		result.Quotes[0].TotalBasePrice = totalBasePrice;
		result.Quotes[0].TotalDiscount = Math.Max( totalDiscount, 0 );
		result.Quotes[0].TotalIncrease = Math.Max( totalIncrease, 0 );

		return (result, restOfBudget);
	}

	private static AbstractBook GetBookEntity( AbstractBook book, int quantity, CustomerSeniorityEnum seniority )
	{
		decimal WholesaleDiscount = CalculateWholesaleDiscount( quantity );
		book.ChangeWholeSaleDiscount( WholesaleDiscount );
		book.ApplyDiscount( seniority );
		return book;
	}

	private static decimal CalculateWholesaleDiscount( int quantity )
	{
		const int MINIMUM_EXPENSIVE_BOOK = 1;
		const int WHOLESALEDISCOUNT_FROM = 10;
		const int DIFFERENCE = WHOLESALEDISCOUNT_FROM - MINIMUM_EXPENSIVE_BOOK;
		const decimal DISCOUNT_RATE = 0.0015m;
		if (quantity >= DIFFERENCE)
		{
			int additionalQuantity = quantity + 1 - DIFFERENCE;
			return DISCOUNT_RATE * additionalQuantity;
		}
		else
		{
			return 0;
		}
	}
}
