using GoArt.Applications.MiniWallet.Domain.ValueTypes;

namespace GoArt.Applications.MiniWallet.Extensions;

public static class DecimalExtensions
{
    public static MoneyAmount ConvertToMoneyAmount(this decimal value)
    {
        int wholePart = 0;
        int pennyPart = 0;

        wholePart = (int)Math.Truncate(value);
        pennyPart = (int)((value - (decimal)wholePart) * 100);

        return new MoneyAmount(wholePart, pennyPart);
    }
}