using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Interfaces;
using LaPinguinera.Quotes.Domain.Model.Quote.Shared;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.GroupQuoteCalculate;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Shared.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Entities;

public class GroupQuoteCalculate : Entity<GroupQuoteCalculateId>
{
	private GroupQuoteCalculate( GroupQuoteCalculateId id ) : base( id )
	{ }

	public GroupQuoteCalculate() : this( new() )
	{ }

	public (IResult result, List<List<AbstractBook>> requestedBooks) CalculateList( List<(string bookId, int quantity)> booksRequested, CustomerSeniorityEnum seniority, List<AbstractBook> inventory )
	{
		List<List<AbstractBook>> requestedBooks = [[]];
		booksRequested.ForEach( ( bookTuple ) =>
		{
			(string bookId, int bookQuantity) = bookTuple;

			AbstractBook bookFromInventory = inventory.Find( book => book.Id.Value == bookId ) ?? throw new KeyNotFoundException( "Book not found" );

			AbstractBook book = bookFromInventory.Clone();

			for (int i = 0; i < bookQuantity; i++)
			{
				requestedBooks[0].Add( book );
			}
		} );

		IResult result = CalculatePrices( requestedBooks, seniority );
		return (result, requestedBooks);
	}

	public (IResult result, List<List<AbstractBook>> requestedBooks) CalculateGroups( List<List<(string bookId, int quantity)>> booksRequested, CustomerSeniorityEnum seniority, List<AbstractBook> inventory )
	{
		IResult result = new Result();
		List<List<AbstractBook>> requestedBooks = [];
		for (int i = 0; i < booksRequested.Count; i++)
		{
			List<(string bookId, int quantity)> group = booksRequested[i];
			for (int j = 0; j < group.Count; j++)
			{
				(string bookId, int bookQuantity) = group[j];
				AbstractBook bookFromInventory = inventory.Find( book => book.Id.Value == bookId ) ?? throw new KeyNotFoundException( "Book not found" );

				AbstractBook book = bookFromInventory.Clone();

				requestedBooks.Add( [] );

				for (int x = 0; x < bookQuantity; x++)
				{
					requestedBooks[j].Add( book );
				}
			}

			IResult groupResult = CalculatePrices( requestedBooks, seniority );

			if (result.Quotes.Count < i + 1) result.Quotes.Add( new GroupQuote() );

			result.Quotes[i] = groupResult.Quotes[0];
			result.Quotes[i] = groupResult.Quotes[0];
			result.Quotes[i].TotalBasePrice = groupResult.TotalBasePrice;
			result.Quotes[i].TotalDiscount = groupResult.TotalDiscount;
			result.Quotes[i].TotalIncrease = groupResult.TotalIncrease;
			result.Quotes[i].TotalPrice = groupResult.TotalPrice;
		}

		result.TotalPrice = result.Quotes.Sum( quote => quote.TotalPrice );
		result.TotalBasePrice = result.Quotes.Sum( quote => quote.TotalBasePrice );
		result.TotalDiscount = result.Quotes.Sum( quote => quote.TotalDiscount );
		result.TotalIncrease = result.Quotes.Sum( quote => quote.TotalIncrease );
		return (result, requestedBooks);
	}

	private static IResult CalculatePrices( List<List<AbstractBook>> requestedBooks, CustomerSeniorityEnum seniority )
	{
		IResult result = new Result();

		for (int i = 0; i < requestedBooks.Count; i++)
		{
			List<AbstractBook> books = requestedBooks[i];

			bool isRetail = books.Count <= 10;

			if (result.Quotes.Count < i + 1) result.Quotes.Add( new GroupQuote() );

			for (int j = 1; j < books.Count; j++)
			{
				AbstractBook book = books[j - 1].Clone();

				const decimal RETAIL_INCREASE = 0.02m;
				const decimal WHOLESALE_DISCOUNT = 0.0015m;
				const int WHOLESALE_DISCOUNT_START = 10;

				book.ChangeRetailIncrease( isRetail ? RETAIL_INCREASE : 0 );
				book.ChangeWholeSaleDiscount( j > WHOLESALE_DISCOUNT_START ? WHOLESALE_DISCOUNT * (j - WHOLESALE_DISCOUNT_START) : 0 );
				book.ApplyDiscount( seniority );

				result.Quotes[i].Books.Add( book.Clone() );
			}
			decimal totalPrice = result.Quotes[i].Books.Sum( book => book.FinalPrice!.Value );
			decimal totalDiscount = result.Quotes[i].Books.Sum( book => book.Discount!.Value );
			decimal totalIncrease = result.Quotes[i].Books.Sum( book => book.Increase!.Value );
			decimal totalBasePrice = result.Quotes[i].Books.Sum( book => book.SellPrice!.Value );

			result.Quotes[i].TotalPrice = totalPrice;
			result.Quotes[i].TotalBasePrice = totalBasePrice;
			result.Quotes[i].TotalDiscount = Math.Max( totalDiscount, 0 );
			result.Quotes[i].TotalIncrease = Math.Max( totalIncrease, 0 );
		}
		result.TotalPrice = result.Quotes.Sum( quote => quote.TotalPrice );
		result.TotalBasePrice = result.Quotes.Sum( quote => quote.TotalBasePrice );
		result.TotalDiscount = result.Quotes.Sum( quote => quote.TotalDiscount );
		result.TotalIncrease = result.Quotes.Sum( quote => quote.TotalIncrease );

		return result;
	}
}
