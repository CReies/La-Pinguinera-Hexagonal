using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.CreationQuoteCalculate;

public class Result : IValueObject<(
	string Id,
	string Title,
	string Author,
	decimal BasePrice,
	decimal FinalPrice,
	BookType Type,
	decimal Discount,
	decimal Increase
)>
{
	public (
		string Id,
		string Title,
		string Author,
		decimal BasePrice,
		decimal FinalPrice,
		BookType Type,
		decimal Discount,
		decimal Increase
		) Value
	{ get; private set; }

	private Result(
		string id,
		string title,
		string author,
		decimal basePrice,
		decimal finalPrice,
		BookType type,
		decimal discount,
		decimal increase
	)
	{
		if (string.IsNullOrWhiteSpace( id ))
		{
			throw new ArgumentException( "Id cannot be null or empty" );
		}

		if (string.IsNullOrWhiteSpace( title ))
		{
			throw new ArgumentException( "Title cannot be null or empty" );
		}

		if (string.IsNullOrWhiteSpace( author ))
		{
			throw new ArgumentException( "Author cannot be null or empty" );
		}

		if (basePrice < 0)
		{
			throw new ArgumentException( "Base price cannot be less than zero" );
		}

		if (finalPrice < 0)
		{
			throw new ArgumentException( "Final price cannot be less than zero" );
		}

		Value = (id, title, author, basePrice, finalPrice, type, discount, increase);
	}

	public static Result Of(
		string id,
		string title,
		string author,
		decimal basePrice,
		decimal finalPrice,
		BookType type,
		decimal discount,
		decimal increase
	)
	{
		return new Result( id, title, author, basePrice, finalPrice, type, discount, increase );
	}
}
