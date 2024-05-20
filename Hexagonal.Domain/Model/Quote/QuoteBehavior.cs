using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Entities;
using LaPinguinera.Quotes.Domain.Model.Quote.Events;
using LaPinguinera.Quotes.Domain.Model.Quote.Factory;
using LaPinguinera.Quotes.Domain.Model.Quote.Shared;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Customer;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;

namespace LaPinguinera.Quotes.Domain.Model.Quote;

public class QuoteBehavior : Behavior
{
	public QuoteBehavior( Quote quote )
	{
		AddQuoteCreatedSub( quote );
		AddCalculateIndividualSub( quote );
		AddCalculateListSub( quote );
		AddCalculateBudgetSub( quote );
		AddCalculateGroupSub( quote );
	}

	private void AddQuoteCreatedSub( Quote quote )
	{
		AddSub( ( DomainEvent @event ) =>
		{
			if (@event is not QuoteCreated) return;
			QuoteCreated? domainEvent = @event as QuoteCreated;
			quote.Result = new Result();
			quote.RequestedBooks = [[]];
			quote.Customer = null;
			quote.Inventory = [];
		} );
	}

	private void AddCalculateIndividualSub( Quote quote )
	{
		AddSub( ( DomainEvent @event ) =>
		{
			if (@event is not IndividualPriceCalculated) return;
			IndividualPriceCalculated domainEvent = (IndividualPriceCalculated)@event;

			ClearResult( quote );

			BookFactory _bookFactory = new();
			AbstractBook book = _bookFactory.Create( domainEvent.BookId, domainEvent.Title, domainEvent.Author, domainEvent.BasePrice, domainEvent.BookType );
			book.CalculateSellPrice();

			quote.Result.Quotes[0].Books.Add( book );

			//quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );
			quote.Inventory.Add( book );
			domainEvent.BookId = book.Id.Value;
		} );
	}

	private void AddCalculateListSub( Quote quote )
	{
		AddSub( ( DomainEvent @event ) =>

		{
			if (@event is not ListPriceCalculated) return;
			ListPriceCalculated domainEvent = (ListPriceCalculated)@event;

			ClearResult( quote );

			domainEvent.BooksRequested.ForEach( ( bookTuple ) =>
			{
				(string bookId, int bookQuantity) = bookTuple;

				AbstractBook bookFromInventory = quote.Inventory.Find( book => book.Id.Value == bookId ) ?? throw new KeyNotFoundException( "Book not found" );

				AbstractBook book = bookFromInventory.Clone();

				for (int i = 0; i < bookQuantity; i++)
				{
					quote.RequestedBooks[0].Add( book );
				}
			} );

			quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );
			_ = quote.Customer.CalculateSeniority();

			CalculatePrices( quote );
		} );
	}

	private static void CalculatePrices( Quote quote )
	{
		for (int i = 0; i < quote.RequestedBooks.Count; i++)
		{
			List<AbstractBook> books = quote.RequestedBooks[i];

			bool isRetail = books.Count <= 10;
			Values.Shared.Enums.CustomerSeniorityEnum seniority = quote.Customer!.Seniority.Value;

			for (int j = 0; j < books.Count; j++)
			{
				AbstractBook book = books[j].Clone();

				book.ChangeRetailIncrease( isRetail ? 0.02m : 0 ); // TODO: Magic number
				book.ChangeWholeSaleDiscount( j > 9 ? 0.0015m * (j - 9) : 0 );
				book.ApplyDiscount( seniority );

				if (quote.Result.Quotes.Count < i + 1) quote.Result.Quotes.Add( new GroupQuote() );

				quote.Result.Quotes[i].Books.Add( book.Clone() );
			}
			decimal totalPrice = quote.Result.Quotes[i].Books.Sum( book => book.FinalPrice!.Value );
			decimal totalSellPrice = quote.Result.Quotes[i].Books.Sum( book => book.SellPrice.Value );
			decimal totalDiscount = totalSellPrice - totalPrice;

			quote.Result.Quotes[i].TotalPrice = totalPrice;
			quote.Result.Quotes[i].TotalDiscount = Math.Max( totalDiscount, 0 );
		}
		quote.Result.TotalPrice = quote.Result.Quotes.Sum( quote => quote.TotalPrice );
		quote.Result.TotalDiscount = quote.Result.Quotes.Sum( quote => quote.TotalDiscount );
	}

	private void AddCalculateBudgetSub( Quote quote )
	{
		AddSub( ( DomainEvent @event ) =>
		{
			if (@event is not BudgetCalculated) return;
			BudgetCalculated domainEvent = (BudgetCalculated)@event;

			ClearResult( quote );

			quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );
			_ = quote.Customer.CalculateSeniority();

			AbstractBook cheapBookFromInventory = quote.Inventory
				.Where( book =>
				book.Data.Value.Type == BookType.BOOK && domainEvent.BookIds.Contains( book.Id.Value ) )
				.OrderBy( book => book.SellPrice.Value )
				.FirstOrDefault() ?? throw new KeyNotFoundException( "You need to request at least one book" );

			AbstractBook cheapNovelFromInventory = quote.Inventory
				.Where( novel => novel.Data.Value.Type == BookType.NOVEL && domainEvent.BookIds.Contains( novel.Id.Value ) )
				.OrderBy( novel => novel.SellPrice.Value )
				.FirstOrDefault() ?? throw new KeyNotFoundException( "You need to request at least one novel" );

			AbstractBook cheapBook = cheapBookFromInventory.Clone();
			AbstractBook cheapNovel = cheapNovelFromInventory.Clone();

			quote.RequestedBooks.Add( [cheapBook, cheapNovel] );

			AbstractBook expensiveBook = cheapBook.SellPrice.Value > cheapNovel.SellPrice.Value ? cheapBook : cheapNovel;
			AbstractBook cheapestBook = cheapBook.SellPrice.Value < cheapNovel.SellPrice.Value ? cheapBook : cheapNovel;

			SetMaxBooksAndRemainingBudget( quote, cheapBook.Clone(), expensiveBook.Clone(), quote.Customer!.Seniority, domainEvent.Budget );
		} );
	}

	private void SetMaxBooksAndRemainingBudget( Quote quote, AbstractBook cheapBook, AbstractBook expensiveBook, CustomerSeniority seniority, decimal totalBudget )
	{
		decimal budget = totalBudget - expensiveBook.SellPrice.Value;

		expensiveBook.ApplyDiscount( seniority.Value );
		quote.Result.Quotes[0].Books.Add( expensiveBook.Clone() );

		decimal restOfBudget = budget;

		int quantity;

		for (quantity = 0; quantity < 600; quantity++)
		{
			AbstractBook bookEntity = GetBookEntity( cheapBook.Clone(), quantity, seniority );

			if (restOfBudget < bookEntity.FinalPrice!.Value) break;

			restOfBudget -= bookEntity.FinalPrice.Value;
			quote.Result.Quotes[0].Books.Add( bookEntity.Clone() );
		}

		if (quantity <= 10) throw new ArgumentException( "You don't have enough budget for a major sale" );

		quote.RestBudget = RestBudget.Of( restOfBudget );
		quote.Result.Quotes[0].TotalPrice = totalBudget - restOfBudget;
		decimal totalSellPrice = quote.Result.Quotes[0].Books.Sum( book => book.SellPrice.Value );
		decimal totalDiscount = totalSellPrice - quote.Result.Quotes[0].TotalPrice;

		quote.Result.TotalDiscount = totalDiscount;
		quote.Result.Quotes[0].TotalDiscount = Math.Max( totalDiscount, 0 );
	}

	private AbstractBook GetBookEntity( AbstractBook book, int quantity, CustomerSeniority seniority )
	{
		decimal WholesaleDiscount = CalculateWholesaleDiscount( quantity );
		book.ChangeWholeSaleDiscount( WholesaleDiscount );
		book.ApplyDiscount( seniority.Value );
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

	private void AddCalculateGroupSub( Quote quote )
	{
		AddSub( ( DomainEvent @event ) =>
		{
			if (@event is not GroupPriceCalculated) return;
			GroupPriceCalculated domainEvent = (GroupPriceCalculated)@event;

			ClearResult( quote );

			quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );

			domainEvent.GroupsRequested.ForEach( ( group ) =>
			{
				for (int i = 0; i < group.Count; i++)
				{
					(string bookId, int bookQuantity) = group[i];
					AbstractBook bookFromInventory = quote.Inventory.Find( book => book.Id.Value == bookId ) ?? throw new KeyNotFoundException( "Book not found" );

					AbstractBook book = bookFromInventory.Clone();

					quote.RequestedBooks.Add( [] );

					for (int j = 0; j < bookQuantity; j++)
					{
						quote.RequestedBooks[i].Add( book );
					}
				}

				quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );
				_ = quote.Customer.CalculateSeniority();

				CalculatePrices( quote );
			} );
		} );
	}

	private static void ClearResult( Quote quote )
	{
		quote.Result = new Result();
		quote.RequestedBooks = [[]];
		quote.Customer = null;
		quote.RestBudget = null;
	}
}
