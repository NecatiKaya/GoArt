using MediatR;

namespace GoArt.Applications.MiniWallet.Features.AddWallet;

public class AddWalletRequest : IRequest<AddWalletResponse>
{
    public string WalletName { get; init; }

    public AddWalletRequest(string name)
    {
        WalletName = name;
    }
}