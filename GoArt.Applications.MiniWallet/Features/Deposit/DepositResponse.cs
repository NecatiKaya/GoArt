using GoArt.Applications.MiniWallet.Domain;

namespace GoArt.Applications.MiniWallet.Features.Deposit;

public class DepositResponse
{
    public Wallet EffectedWallet { get; set; }

    public DepositResponse(Wallet effectedWallet)
    {
        EffectedWallet = effectedWallet;
    }
}