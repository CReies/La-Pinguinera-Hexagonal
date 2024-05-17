using LaPinguinera.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Entities;
using LaPinguinera.Quotes.Domain.Model.Quote.Events;
using LaPinguinera.Quotes.Domain.Model.Quote.Factory;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;
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
	}

	private void AddQuoteCreatedSub( Quote quote )
	{
		AddSub( ( QuoteCreated domainEvent ) =>
		{
			quote.Result = ([], null, null);
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

			quote.Result.Quote.Add( ([book], null, null) );
			quote.Customer = Customer.From( RegisterDate.Of( domainEvent.CustomerRegisterDate ) );
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
		var books = quote.RequestedBooks[0];
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
	}
}
