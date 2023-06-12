using GoArt.Applications.MiniWallet.Domain.ValueTypes;

namespace GoArt.Applications.MiniWallet.Domain;

public interface ICurrencyConverter
{
    MoneyAmount GetExchangeRate(MoneyAmountWithCurrency from, Currency to);
}