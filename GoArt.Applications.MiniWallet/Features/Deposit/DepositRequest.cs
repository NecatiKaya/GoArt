using GoArt.Applications.MiniWallet.Domain.ValueTypes;
using MediatR;

namespace GoArt.Applications.MiniWallet.Features.Deposit;

public class DepositRequest : IRequest<DepositResponse>
{
    public MoneyAmountWithCurrency Amount { get; set; }

    public WalletId WalletToAddDeposit { get; set; }

    public DepositRequest(WalletId walletId, MoneyAmountWithCurrency amount)
    {
        WalletToAddDeposit = walletId;
        Amount = amount;
    }
}