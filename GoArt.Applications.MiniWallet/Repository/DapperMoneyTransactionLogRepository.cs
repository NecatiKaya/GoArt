using GoArt.Applications.MiniWallet.Core.Data;
using GoArt.Applications.MiniWallet.Domain.ValueTypes;

namespace GoArt.Applications.MiniWallet.Repository;

public class DapperMoneyTransactionLogRepository : IMoneyTransactionLogRepository
{
    private readonly DapperContext _dapperContext;

    public DapperMoneyTransactionLogRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public Task AddLog(WalletId walletId)
    {
        throw new NotImplementedException();
    }
}