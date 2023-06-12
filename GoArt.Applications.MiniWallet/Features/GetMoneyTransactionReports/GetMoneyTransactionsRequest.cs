using GoArt.Applications.MiniWallet.Domain.ValueTypes;
using MediatR;

namespace GoArt.Applications.MiniWallet.Features.GetMoneyTransactionReports;

public class GetMoneyTransactionsRequest : IRequest<GetMoneyTransactionsReponse>
{
    public WalletId WalletId { get; set; }

    public GetMoneyTransactionsRequest(WalletId walletId)
    {
        WalletId = walletId;
    }
}