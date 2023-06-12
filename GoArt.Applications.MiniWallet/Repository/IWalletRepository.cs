using GoArt.Applications.MiniWallet.Domain;
using GoArt.Applications.MiniWallet.Domain.ValueTypes;

namespace GoArt.Applications.MiniWallet.Repository;

public interface IWalletRepository
{
    Task<Wallet?> GetWalletById(WalletId walletId);

    Task CreateWallet(WalletId id, string name);

    Task<Guid> Deposit(WalletId walletId, MoneyAmountWithCurrency amount);

    Task<Guid> Withdraw(WalletId walletId, MoneyAmountWithCurrency amount);
}