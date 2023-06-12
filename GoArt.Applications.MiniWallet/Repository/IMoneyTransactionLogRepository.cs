using GoArt.Applications.MiniWallet.Domain.ValueTypes;

namespace GoArt.Applications.MiniWallet.Repository;

public interface IMoneyTransactionLogRepository
{
    Task AddLog(WalletId walletId);
}