using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.BudgetQuoteCalculate;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Shared.Enums;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Shared.Helper;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Entities;

public class BudgetQuoteCalculate : Entity<BudgetQuoteCalculateId>
{
	public Result Result { get; set; }

	private BudgetQuoteCalculate( BudgetQuoteCalculateId id ) : base( id )
	{ }

	public BudgetQuoteCalculate() : this( new() )
	{ }

	public void Calculate( List<AbstractBook> inventory, List<string> requestedBookIds, CustomerSeniorityEnum seniority, decimal budget )
	{
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

		AbstractBook expensiveBook = cheapBook.SellPrice.Value > cheapNovel.SellPrice.Value ? cheapBook : cheapNovel;
		AbstractBook cheapestBook = cheapBook.SellPrice.Value < cheapNovel.SellPrice.Value ? cheapBook : cheapNovel;

		SetMaxBooksAndRemainingBudget( cheapBook.Clone(), expensiveBook.Clone(), seniority, budget );
	}

	private void SetMaxBooksAndRemainingBudget( AbstractBook cheapBook, AbstractBook expensiveBook, CustomerSeniorityEnum seniority, decimal totalBudget )
	{
		decimal budget = totalBudget - expensiveBook.SellPrice.Value;
		List<AbstractBook> books = [];

		expensiveBook.ApplyDiscount( seniority );
		books.Add( expensiveBook.Clone() );

		decimal restOfBudget = budget;

		int quantity;

		for (quantity = 0; quantity < 600; quantity++)
		{
			AbstractBook bookEntity = GetBookEntity( cheapBook.Clone(), quantity, seniority );

			if (restOfBudget < bookEntity.FinalPrice!.Value) break;

			restOfBudget -= bookEntity.FinalPrice.Value;
			books.Add( bookEntity.Clone() );
		}

		if (quantity <= 10) throw new ArgumentException( "You don't have enough budget for a major sale" );

		decimal totalPrice = totalBudget - restOfBudget;

		decimal totalBasePrice = books.Sum( book => book.SellPrice!.Value );
		decimal totalDiscount = Math.Max( 0, books.Sum( book => book.Discount!.Value ) );
		decimal totalIncrease = Math.Max( 0, books.Sum( book => book.Increase!.Value ) );
		int totalBooks = books.Count( book => book.Data.Value.Type == BookType.BOOK );
		int totalNovels = books.Count( book => book.Data.Value.Type == BookType.NOVEL );

		List<BookResult> booksForResult = AbstractBookToBookResult( books );

		Result = Result.Of( booksForResult, totalPrice, totalBasePrice, totalDiscount, totalIncrease, restOfBudget, totalBooks, totalNovels );
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

	private List<BookResult> AbstractBookToBookResult( List<AbstractBook> books )
	{
		return books.Select( book => new BookResult(
			book.Id.Value,
			book.Data.Value.Title,
			book.Data.Value.Author,
			book.SellPrice!.Value,
			book.FinalPrice!.Value,
			book.Data.Value.Type,
			book.Discount!.Value,
			book.Increase!.Value
			) ).ToList();
	}
}
