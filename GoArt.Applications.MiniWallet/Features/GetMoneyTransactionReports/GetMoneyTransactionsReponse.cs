using GoArt.Applications.MiniWallet.Domain;
using GoArt.Applications.MiniWallet.Domain.ValueTypes;

namespace GoArt.Applications.MiniWallet.Features.GetMoneyTransactionReports;

public class GetMoneyTransactionsReponse
{
    public IReadOnlyList<MoneyTransaction> Transactions { get; set; }

    public GetMoneyTransactionsReponse(IReadOnlyList<MoneyTransaction> transactions)
    {
        Transactions = transactions;
    }
}