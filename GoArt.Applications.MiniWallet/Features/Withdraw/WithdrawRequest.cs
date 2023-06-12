using GoArt.Applications.MiniWallet.Domain.ValueTypes;
using MediatR;

namespace GoArt.Applications.MiniWallet.Features.Withdraw;

public class WithdrawRequest : IRequest<WithdrawResponse>
{
    public MoneyAmountWithCurrency Amount { get; set; }

    public WalletId WalletToWithdraw { get; set; }

    public WithdrawRequest(WalletId walletId, MoneyAmountWithCurrency amount)
    {
        WalletToWithdraw = walletId;
        Amount = amount;
    }
}