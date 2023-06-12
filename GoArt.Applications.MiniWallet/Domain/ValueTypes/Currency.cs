using GoArt.Applications.MiniWallet.Core.Problem;

namespace GoArt.Applications.MiniWallet.Domain.ValueTypes;

public record Currency
{
    private static string[] _supportedCurrencies = new string[] { "TRY", "USD", "EUR", "GBP" };

    public string CurrencyCode { get; init; }

    private Currency(string currencyCode)
    {
        CurrencyCode = currencyCode;
    }

    public static Currency TurkishLira = Currency.Create("TRY");

    public static Currency Dollar = Currency.Create("USD");

    public static Currency Euro = Currency.Create("EUR");

    public static Currency Pound = Currency.Create("GBP");

    public static Currency Create(string currencyCode)
    {
        bool isAvailableCurrency = _supportedCurrencies.Contains(currencyCode);

        if (!isAvailableCurrency)
        {
            throw new ProblemException(Problem.Create(MiniWalletErrorCodes.NOT_A_SUPPORTED_CURRENCY));
        }

        return new Currency(currencyCode);
    }

    public override string ToString()
    {
        return CurrencyCode;
    }
}