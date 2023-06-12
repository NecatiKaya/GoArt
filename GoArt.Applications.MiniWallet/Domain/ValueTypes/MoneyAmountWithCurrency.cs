namespace GoArt.Applications.MiniWallet.Domain.ValueTypes;

public record MoneyAmountWithCurrency
{
    public MoneyAmount Amount { get; init; }

    public Currency Currency { get; init; }

    public MoneyAmountWithCurrency(MoneyAmount moneyAmount, Currency currency)
    {
        Amount = moneyAmount;
        Currency = currency;
    }

    public override string ToString()
    {
        return string.Format("{0} {1}", Amount.Value.ToString(), Currency.CurrencyCode);
    }
}