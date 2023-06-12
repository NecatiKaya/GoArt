using GoArt.Applications.MiniWallet.Core.Problem;

namespace GoArt.Applications.MiniWallet.Domain.ValueTypes;

public record MoneyAmount
{
    public decimal Value { get; private set; }

    /// <summary>
    /// Gets or sets whole part of the total amount. For example the amount is 17.95, this prop will return 17
    /// </summary>
    public int WholePart { get; private set; }

    /// <summary>
    /// Gets or sets penny part of the total amount. For example the amount is 17.95, this prop will return 95
    /// </summary>
    public int PennyPart { get; private set; }

    public MoneyAmount(int wholePart, int pennies)
    {
        if (wholePart < 0)
        {
            throw new ProblemException(Problem.Create(MiniWalletErrorCodes.NOT_A_VALID_MONEY_AMOUNT));
        }

        if (pennies < 0)
        {
            throw new ProblemException(Problem.Create(MiniWalletErrorCodes.NOT_A_VALID_MONEY_AMOUNT));
        }

        if (wholePart == 0 && pennies == 0)
        {
            throw new ProblemException(Problem.Create(MiniWalletErrorCodes.NOT_A_VALID_MONEY_AMOUNT));
        }

        if (pennies >= 100)
        {
            throw new ProblemException(Problem.Create(MiniWalletErrorCodes.NOT_A_VALID_MONEY_AMOUNT));
        }

        Value = ((decimal)wholePart) + ((decimal)pennies / 100m);
        WholePart = wholePart;
        PennyPart = pennies;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}