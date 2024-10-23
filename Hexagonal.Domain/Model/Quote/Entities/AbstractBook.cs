using LaPinguinera.Quotes.Domain.Generic;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;
using LaPinguinera.Quotes.Domain.Model.Quote.Values.Shared.Enums;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Entities;

public abstract class AbstractBook : Entity<BookId>
{
	public Data Data { get; protected set; }
	public RetailIncrease? RetailIncrease { get; protected set; }
	public WholeSaleDiscount? WholeSaleDiscount { get; protected set; }
	public SeniorityDiscount? SeniorityDiscount { get; protected set; }
	public BaseIncrease BaseIncrease { get; protected set; }
	public BasePrice BasePrice { get; protected set; }
	public SellPrice SellPrice { get; protected set; }
	public FinalPrice? FinalPrice { get; protected set; }
	public Discount? Discount { get; protected set; }
	public Increase? Increase { get; protected set; }

	protected AbstractBook( BookId id, Data data, BaseIncrease baseIncrease, BasePrice basePrice ) : base( id )
	{
		Data = data;
		BaseIncrease = baseIncrease;
		BasePrice = basePrice;
	}

	protected AbstractBook( Data data, BaseIncrease baseIncrease, BasePrice basePrice ) :
		this( new(), data, baseIncrease, basePrice )
	{ }

	public void CalculateSellPrice()
	{
		decimal sellPrice = BasePrice.Value * (1 + BaseIncrease.Value);
		decimal increase = sellPrice - BasePrice.Value;
		decimal discount = 0;

		SellPrice = SellPrice.Of( sellPrice );
		Increase = Increase.Of( increase );
		Discount = Discount.Of( discount );
	}

	public void ApplyDiscount( CustomerSeniorityEnum customerSeniority )
	{
		Dictionary<CustomerSeniorityEnum, decimal> seniorityDiscounts = new()
		{
			{ CustomerSeniorityEnum.LessOneYear, 0 },
			{ CustomerSeniorityEnum.OneToTwoYears, 0.12m },
			{ CustomerSeniorityEnum.MoreTwoYears, 0.17m }
		};

		if (RetailIncrease is null) ChangeRetailIncrease( 0 );
		if (WholeSaleDiscount is null) ChangeWholeSaleDiscount( 0 );

		decimal seniorDiscount = seniorityDiscounts[customerSeniority];
		decimal priceWithIncrease = SellPrice.Value * (1 + RetailIncrease!.Value);
		Increase = Increase.Of( priceWithIncrease - SellPrice.Value );

		decimal finalPrice = priceWithIncrease * (1 - WholeSaleDiscount!.Value) * (1 - seniorDiscount);
		Discount = Discount.Of( priceWithIncrease - finalPrice );

		FinalPrice = FinalPrice.Of( finalPrice );
	}

	public abstract AbstractBook Clone();

	public void ChangeRetailIncrease( decimal value )
	{
		RetailIncrease = RetailIncrease.Of( value );
	}

	public void ChangeWholeSaleDiscount( decimal value )
	{
		WholeSaleDiscount = WholeSaleDiscount.Of( value );
	}

	public void ChangeSeniorityDiscount( decimal value )
	{
		SeniorityDiscount = SeniorityDiscount.Of( value );
	}

	public void ChangeSellPrice( decimal value )
	{
		SellPrice = SellPrice.Of( value );
	}

	public void ChangeFinalPrice( decimal value )
	{
		FinalPrice = FinalPrice.Of( value );
	}
}
