using GoArt.Applications.MiniWallet.Domain.ValueTypes;

namespace GoArt.Applications.MiniWallet.Features.AddWallet;

public class AddWalletResponse 
{
    public WalletId Id { get; init; }

    public string Name { get; set; }

    public AddWalletResponse(WalletId id, string name)
    {
        Id = id;
        Name = name;
    }
}