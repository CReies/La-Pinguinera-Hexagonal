using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Entities;
using LaPinguinera.Quotes.Domain.Model.Quote.Events;
using LaPinguinera.Quotes.Domain.Model.Quote.Factory;
using LaPinguinera.Quotes.Domain.Model.Quote.Shared;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Customer;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Root;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Shared.Enums;

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
		AddSub( ( QuoteCreated domainEvent ) =>
		{
			quote.Result = new Result();
			quote.RequestedBooks = [];
			quote.Customer = null;
			quote.Inventory = [];
		} );
	}

	private void AddCalculateIndividualSub( Quote quote )
	{
		AddSub( ( IndividualPriceCalculated domainEvent ) =>
		{
			BookFactory _bookFactory = new();
			var book = BookFactory.Create( domainEvent.Title, domainEvent.Author, domainEvent.BasePrice, domainEvent.BookType );
			book.CalculateSellPrice();

			quote.Result.Quotes[0].Books.Add( book );
			//quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );
			quote.Inventory.Add( book );
		} );
	}

	private void AddCalculateListSub( Quote quote )
	{
		AddSub( ( ListPriceCalculated domainEvent ) =>
		{
			domainEvent.BooksRequested.ForEach( ( bookTuple ) =>
			{
				var (bookId, bookQuantity) = bookTuple;

				var book = quote.Inventory.Find( book => book.Id.Value == bookId ) ?? throw new KeyNotFoundException( "Book not found" );
				for (int i = 0; i < bookQuantity; i++)
				{
					quote.RequestedBooks[0].Add( book );
				}
			} );

			quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );
			quote.Customer.CalculateSeniority();

			CalculatePrices( quote );
		} );
	}

	private static void CalculatePrices( Quote quote )
	{
		quote.RequestedBooks.ForEach( books =>
		{
			var isRetail = books.Count <= 10;
			var seniority = quote.Customer!.Seniority.Value;

			for (int i = 0; i < books.Count; i++)
			{
				var book = books[i];

				book.ChangeRetailIncrease( isRetail ? 0.02m : 0 );
				book.ChangeWholeSaleDiscount( i > 9 ? 0.0015m * (i - 9) : 0 );
				book.ApplyDiscount( seniority );

				book.ApplyDiscount( seniority );
			}
		} );
	}

	private void AddCalculateBudgetSub( Quote quote )
	{
		AddSub( ( BudgetCalculated domainEvent ) =>
		{
			quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );

			var cheapBook = quote.Inventory
				.Where( book => book.Data.Value.Type == BookType.BOOK )
				.OrderBy( book => book.SellPrice.Value )
				.First();

			var cheapNovel = quote.Inventory
				.Where( book => book.Data.Value.Type == BookType.NOVEL )
				.OrderBy( book => book.SellPrice.Value )
				.First();

			var expensiveBook = cheapBook.SellPrice.Value > cheapNovel.SellPrice.Value ? cheapBook : cheapNovel;
			var cheapestBook = cheapBook.SellPrice.Value < cheapNovel.SellPrice.Value ? cheapBook : cheapNovel;

			SetMaxBooksAndRemainingBudget( quote, cheapBook, expensiveBook, quote.Customer!.Seniority, domainEvent.Budget );
		} );
	}

	private void SetMaxBooksAndRemainingBudget( Quote quote, AbstractBook cheapBook, AbstractBook expensiveBook, CustomerSeniority seniority, decimal totalBudget )
	{
		var budget = totalBudget - expensiveBook.SellPrice.Value;
		var restOfBudget = budget;

		int quantity;

		for (quantity = 0; quantity < 600; quantity++)
		{
			var bookEntity = GetBookEntity( cheapBook, quantity, seniority );

			if (restOfBudget < bookEntity.FinalPrice!.Value) break;

			restOfBudget -= bookEntity.FinalPrice.Value;
		}

		if (quantity <= 10) throw new ArgumentException( "You don't have enough budget for a major sale" );
		quote.Result.Quotes[0].Books.Add( expensiveBook );

		for (int i = 0; i < quantity; i++)
		{
			quote.Result.Quotes[0].Books.Add( cheapBook );
		}

		quote.RestBudget = RestBudget.Of( restOfBudget );
		quote.Result.Quotes[0].TotalPrice = totalBudget - restOfBudget;
		var totalDiscount = quote.Result.Quotes[0].Books.Sum( book => book.SellPrice.Value ) - quote.Result.Quotes[0].TotalPrice;
	}

	private AbstractBook GetBookEntity( AbstractBook book, int quantity, CustomerSeniority seniority )
	{
		var WholesaleDiscount = CalculateWholesaleDiscount( quantity );
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
		AddSub( ( GroupPriceCalculated domainEvent ) =>
		{
			quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );

			domainEvent.GroupsRequested.ForEach( ( group ) =>
			{
				for (int i = 0; i < group.Count; i++)
				{
					var (bookId, bookQuantity) = group[i];
					var book = quote.Inventory.Find( book => book.Id.Value == bookId ) ?? throw new KeyNotFoundException( "Book not found" );
					for (int j = 0; j < bookQuantity; j++)
					{
						quote.RequestedBooks[i].Add( book );
					}
				}

				quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );
				quote.Customer.CalculateSeniority();

				CalculatePrices( quote );
			} );
		} );
	}
}
