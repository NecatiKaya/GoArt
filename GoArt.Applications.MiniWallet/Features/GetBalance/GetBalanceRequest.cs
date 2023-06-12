using GoArt.Applications.MiniWallet.Domain.ValueTypes;
using MediatR;

namespace GoArt.Applications.MiniWallet.Features.GetBalance;

public class GetBalanceRequest : IRequest<GetBalanceResponse>
{
    public Currency Currency { get; set; }

    public WalletId WalletId { get; set; }

    public GetBalanceRequest(Currency currency, WalletId walletId)
    {
        Currency = currency;
        WalletId = walletId;
    }
}