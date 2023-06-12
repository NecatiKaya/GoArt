using GoArt.Applications.MiniWallet.Domain;

namespace GoArt.Applications.MiniWallet.Features.Withdraw;

public class WithdrawResponse
{
    public Wallet EffectedWallet { get; set; }

    public WithdrawResponse(Wallet effectedWallet)
    {
        EffectedWallet = effectedWallet;
    }
}
