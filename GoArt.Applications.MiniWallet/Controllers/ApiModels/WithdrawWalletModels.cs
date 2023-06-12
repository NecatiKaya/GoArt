using FluentValidation;
using GoArt.Applications.MiniWallet.Domain;

namespace GoArt.Applications.MiniWallet.Controllers.ApiModels;

public class WithdrawApiRequest
{
    public int WholePart { get; set; }

    public int PennyPart { get; set; }

    public string Currency { get; set; } = null!;
}

public class WithdrawApiRequestValidator : AbstractValidator<WithdrawApiRequest>
{
    public WithdrawApiRequestValidator()
    {
        RuleFor(model => model.WholePart).GreaterThanOrEqualTo(0).WithErrorCode(MiniWalletErrorCodes.NOT_A_VALID_MONEY_AMOUNT);

        RuleFor(model => model.PennyPart).GreaterThanOrEqualTo(0).WithErrorCode(MiniWalletErrorCodes.NOT_A_VALID_MONEY_AMOUNT);

        RuleFor(model => model.Currency).NotNull().NotEmpty().WithErrorCode(MiniWalletErrorCodes.NOT_A_SUPPORTED_CURRENCY);
    }
}