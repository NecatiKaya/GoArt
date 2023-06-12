using GoArt.Applications.MiniWallet.Domain;

namespace GoArt.Applications.MiniWallet.Features.GetWalletById;

public class GetWalletByIdResponse
{
    public Wallet Wallet { get; set; }

    public GetWalletByIdResponse(Wallet wallet)
    {
        Wallet = wallet;
    }
}