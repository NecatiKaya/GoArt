namespace GoArt.Applications.MiniWallet.Domain.ValueTypes;

public record MoneyTransaction
{
    public MoneyAmount Amount { get; init; }

    public Currency Currency { get; init; }

    public MoneyTransactionType TransactionType { get; init; }

    public DateTime TransactionDate { get; init; } = DateTime.Now;

    public string Translation
    {
        get
        {
            return this.ToString();
        }
    }

    public MoneyTransaction(MoneyAmount amount, Currency currency, MoneyTransactionType transactionType, DateTime transactionDate)
    {
        Amount = amount;
        Currency = currency;
        TransactionType = transactionType;
        TransactionDate = transactionDate;
    }

    public override string ToString()
    {
        return string.Format("{0}{1} {2}",
            TransactionType ==  MoneyTransactionType.Withdraw ? "-": "",
            Amount.ToString(),
            Currency);
    }
}