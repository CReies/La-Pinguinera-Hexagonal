using LaPinguinera.Domain.Generic;

namespace LaPinguinera.Quotes.Domain.Model.Quote.Values.Book;

public class PriceModifiers : IValueObject<(
    decimal RetailIncrease,
    decimal WholeSaleDiscount,
    decimal SeniorDiscount
)>
{
    public (decimal RetailIncrease, decimal WholeSaleDiscount, decimal SeniorDiscount) Value { get; private set; }
    private PriceModifiers((decimal RetailIncrease, decimal WholeSaleDiscount, decimal SeniorDiscount) value)
    {
        if (value.RetailIncrease < 0)
        {
            throw new ArgumentException("Retail increase cannot be less than zero");
        }

        if (value.WholeSaleDiscount < 0)
        {
            throw new ArgumentException("Whole sale discount cannot be less than zero");
        }

        if (value.SeniorDiscount < 0)
        {
            throw new ArgumentException("Senior discount cannot be less than zero");
        }

        Value = value!;
    }

    public static PriceModifiers Of(decimal retailIncrease, decimal wholeSaleDiscount, decimal seniorDiscount)
    {
        return new PriceModifiers((retailIncrease, wholeSaleDiscount, seniorDiscount));
    }
}
