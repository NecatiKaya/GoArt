using GoArt.Applications.MiniWallet.Core.Problem;
using GoArt.Applications.MiniWallet.Domain.ValueTypes;
using GoArt.Applications.MiniWallet.Localization;

namespace GoArt.Applications.MiniWallet.Domain;

public sealed class Wallet
{
    private MoneyTransactionCollection _transactions = new MoneyTransactionCollection();

    public string WalletName { get; init; }

    public WalletId Id { get; init; }

    private Wallet(Guid id, string name, MoneyTransactionCollection transactions)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ProblemException(Problem.Create(MiniWalletErrorCodes.WALLET_NAME_NOT_SPECIFIED));
        }

        Id = WalletId.Create(id);
        WalletName = name;
        _transactions = transactions;
    }

    public List<MoneyTransaction> Transactions()
    {
        return _transactions.TransactionsAsReadonly().ToList();
    }

    public static Wallet CreateWallet(Guid id, string name, MoneyTransactionCollection transactions)
    {
        Wallet wallet = new Wallet(id, name, transactions);
        return wallet;
    }

    public WalletOperationResponse CanDeposit(Currency currency, MoneyAmount amount)
    {
        /*
            There might be business cases where it is not allowed to deposit. Might be blocked wallet or sth else.
         */
        return WalletOperationResponse.Success();
    }

    public void Deposit(Currency currency, MoneyAmount amount)
    {
        WalletOperationResponse canDeposit = CanDeposit(currency, amount);
        if (!canDeposit.IsSuccess)
        {
            throw new ProblemException(Problem.Create(MiniWalletErrorCodes.NOT_ENOUGH_AMOUNT_IN_WALLET_FOR_CURRENCY));
        }

        _transactions.Add(new MoneyTransaction(amount, currency, MoneyTransactionType.Deposit, DateTime.Now));
    }

    /// <summary>
    /// Firstly, requested amount is tried to withdraw from requested currency. If there is not enough for amount for the currency, than all amount in the wallet converted to requested currency than the operation is tried
    /// </summary>
    /// <param name="currency">Requested currency insid wallet</param>
    /// <param name="amount">Requested amount in wallet to withdraw</param>
    /// <exception cref="ProblemException"></exception>
    public WalletOperationResponse CanWithdraw(Currency currency, MoneyAmount amount, ICurrencyConverter currencyConverter)
    {
        IReadOnlyList<MoneyTransaction> transactions = _transactions.TransactionsAsReadonly(currency);
        if (!transactions.Any())
        {
            return WalletOperationResponse.Fail(Problem.Create(MiniWalletErrorCodes.NOT_ENOUGH_AMOUNT_IN_WALLET_FOR_CURRENCY));
        }

        //Get all transactions
        transactions = _transactions.TransactionsAsReadonly();

        if (!transactions.Any())
        {
            return WalletOperationResponse.Fail(Problem.Create(MiniWalletErrorCodes.NO_AMOUNT_IN_WALLET));
        }

        MoneyAmountWithCurrency moneyAmountWithCurrency = _transactions.GetTotalAmount(currency, currencyConverter);
        if (moneyAmountWithCurrency.Amount.Value <= 0 || moneyAmountWithCurrency.Amount.Value < amount.Value)
        {
            return WalletOperationResponse.Fail(Problem.Create(MiniWalletErrorCodes.NOT_ENOUGN_AMOUNT_IN_WALLET));
        }

        return WalletOperationResponse.Success();
    }

    public void Withdraw(Currency currency, MoneyAmount amount, ICurrencyConverter currencyConverter)
    {
        WalletOperationResponse canWithdraw = CanWithdraw(currency, amount, currencyConverter);
        if (!canWithdraw.IsSuccess)
        {
            throw new ProblemException(canWithdraw.Problems!.First());
        }
        _transactions.Add(new MoneyTransaction(amount, currency, MoneyTransactionType.Withdraw, DateTime.Now));
    }

    public MoneyAmountWithCurrency Balance(Currency balanceCurrency, ICurrencyConverter currencyConverter)
    {
        MoneyAmountWithCurrency balance = _transactions.GetTotalAmount(balanceCurrency, currencyConverter);
        return balance;
    }
}