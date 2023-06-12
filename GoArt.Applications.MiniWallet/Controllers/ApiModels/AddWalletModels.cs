using FluentValidation;
using GoArt.Applications.MiniWallet.Domain;

namespace GoArt.Applications.MiniWallet.Controllers.ApiModels;

public class AddWalletApiRequest
{
    public string WalletName { get; set; } = null!;
}

public class AddWalletApiRequestValidator : AbstractValidator<AddWalletApiRequest>
{
    public AddWalletApiRequestValidator()
    {
        RuleFor(model => model.WalletName).NotNull().NotEmpty().WithErrorCode(MiniWalletErrorCodes.WALLET_NAME_NOT_SPECIFIED);
    }
}