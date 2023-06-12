using FluentValidation;
using GoArt.Applications.MiniWallet.Domain;
using GoArt.Applications.MiniWallet.Domain.ValueTypes;

namespace GoArt.Applications.MiniWallet.Controllers.ApiModels;

public class DepositApiRequest
{
    public int WholePart { get; set; }

    public int PennyPart { get; set; }

    public string Currency { get; set; } = null!;
}

public class DepositApiRequestValidator : AbstractValidator<DepositApiRequest>
{
    public DepositApiRequestValidator()
    {
        RuleFor(model => model.WholePart).GreaterThanOrEqualTo(0).WithErrorCode(MiniWalletErrorCodes.NOT_A_VALID_MONEY_AMOUNT);

        RuleFor(model => model.PennyPart).GreaterThanOrEqualTo(0).WithErrorCode(MiniWalletErrorCodes.NOT_A_VALID_MONEY_AMOUNT);

        RuleFor(model => model.Currency).NotNull().NotEmpty().WithErrorCode(MiniWalletErrorCodes.NOT_A_SUPPORTED_CURRENCY);
    }
}