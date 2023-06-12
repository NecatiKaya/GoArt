using GoArt.Applications.MiniWallet.Domain.ValueTypes;

namespace GoArt.Applications.MiniWallet.Features.GetBalance;

public class GetBalanceResponse
{
    public MoneyAmountWithCurrency Amount { get; set; }

    public GetBalanceResponse(MoneyAmountWithCurrency amount)
    {
        Amount = amount;
    }
}