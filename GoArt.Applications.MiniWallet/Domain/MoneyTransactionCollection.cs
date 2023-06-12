using GoArt.Applications.MiniWallet.Domain.ValueTypes;
using GoArt.Applications.MiniWallet.Extensions;

namespace GoArt.Applications.MiniWallet.Domain;

public class MoneyTransactionCollection
{
    private readonly List<MoneyTransaction> _transactions = new List<MoneyTransaction>();

    public MoneyTransactionCollection()
    {

    }

    public MoneyTransactionCollection(IEnumerable<MoneyTransaction> moneyTransactions)
    {
        _transactions.AddRange(moneyTransactions);
    }

    public void Add(MoneyTransaction moneyTransaction)
    {
        _transactions.Add(moneyTransaction);
    }

    public IReadOnlyList<MoneyTransaction> TransactionsAsReadonly()
    {
        return _transactions.AsReadOnly();
    }

    /// <summary>
    /// Returns only transactions related to a currency
    /// </summary>
    /// <param name="currency">Currency to get its transactions</param>
    /// <returns>Returns list of transactions as a readonly list</returns>
    public IReadOnlyList<MoneyTransaction> TransactionsAsReadonly(Currency currency)
    {
        return _transactions.Where(eachTransaction => eachTransaction.Currency == currency).ToList().AsReadOnly();
    }

    public MoneyAmountWithCurrency GetTotalAmount(Currency requestedCurrency, ICurrencyConverter converter)
    {
        if (!_transactions.Any())
        {
            return new MoneyAmountWithCurrency(new MoneyAmount(0, 0), requestedCurrency);
        }

        decimal totalValue = 0;
        foreach (MoneyTransaction eachTransaction in _transactions)
        {
            MoneyAmount convertedAmount = converter.GetExchangeRate(new MoneyAmountWithCurrency(eachTransaction.Amount, eachTransaction.Currency), requestedCurrency);
            if (eachTransaction.TransactionType == MoneyTransactionType.Deposit)
            {
                totalValue += convertedAmount.Value;
            }
            else
            {
                totalValue -= convertedAmount.Value;
            }
        }

        return new MoneyAmountWithCurrency(totalValue.ConvertToMoneyAmount(), requestedCurrency);
    }
}