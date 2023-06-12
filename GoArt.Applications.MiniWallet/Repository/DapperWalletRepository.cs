using System.Data;
using Dapper;
using GoArt.Applications.MiniWallet.Core.Data;
using GoArt.Applications.MiniWallet.Domain;
using GoArt.Applications.MiniWallet.Domain.ValueTypes;

namespace GoArt.Applications.MiniWallet.Repository;

public class DapperWalletRepository : IWalletRepository
{
    private readonly DapperContext _dapperContext;

    private readonly IMoneyTransactionLogRepository _logRepository;

    public DapperWalletRepository(DapperContext dapperContext, IMoneyTransactionLogRepository logRepository)
    {
        _dapperContext = dapperContext;
        _logRepository = logRepository;
    }

    public async Task CreateWallet(WalletId id, string name)
    {
        await _dapperContext.RunWithoutTransactionAsync(async (sqlConnection) =>
        {
            string query = "INSERT INTO dbo.Wallets ([Id], [Name]) VALUES (@Id, @Name)";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id.Value, DbType.Guid);
            parameters.Add("Name", name, DbType.String, size: 50);

            await sqlConnection.ExecuteAsync(query, parameters);
        });
    }

    public async Task<Wallet?> GetWalletById(WalletId walletId)
    {
        Wallet? wallet = await _dapperContext.RunWithoutTransactionAsync<Wallet?>(async (sqlConnection) =>
        {
            string sql = @"
                                                SELECT [Id], [Name] FROM dbo.Wallets AS W WITH(NOLOCK) WHERE Id = @Id;

                                                SELECT MT.[Id]
                                                      ,MT.[Amount]
                                                      ,MT.[Amount_Whole_Part]
                                                      ,MT.[Amount_Penny_Part]
                                                      ,MT.[Currency]
                                                      ,MT.[TransactionType]
                                                      ,MT.[TransactionDate]
                                                FROM [dbo].[Wallets] AS W WITH(NOLOCK)
                                                LEFT JOIN dbo.MoneyTransactions AS MT WITH(NOLOCK) ON W.Id = MT.WalletId
                                                WHERE W.Id = @Id
                                                ORDER BY MT.[TransactionDate] DESC;";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", walletId.Value, DbType.Guid);

            using (var multi = await sqlConnection.QueryMultipleAsync(sql, parameters))
            {
                WalletRawDbData? walletRawData = await multi.ReadSingleOrDefaultAsync<WalletRawDbData?>();
                IEnumerable<MoneyTransaction>? moneyTransactions = (await multi.ReadAsync<MoneyTransactionRawDbData>()).
                Where(eachTransaction=> eachTransaction.Id != Guid.Empty).
                Select((eachTransactionRawData) => {
                    return new MoneyTransaction(
                        new MoneyAmount(eachTransactionRawData.Amount_Whole_Part, eachTransactionRawData.Amount_Penny_Part),
                        Currency.Create(eachTransactionRawData.Currency),
                        eachTransactionRawData.TransactionType,
                        eachTransactionRawData.TransactionDate);
                });

                if (walletRawData is null)
                {
                    return null;
                }

                Wallet _tempWallet = Wallet.CreateWallet(walletRawData.Id, walletRawData.Name, new MoneyTransactionCollection(moneyTransactions));
                return _tempWallet;
            }
        });

        return wallet;
    }

    public async Task<Guid> Deposit(WalletId walletId, MoneyAmountWithCurrency amount)
    {
        Guid transactionId = Guid.NewGuid();

        await _dapperContext.RunWithoutTransactionAsync(async (sqlConnection) =>
        {
            string query = "INSERT INTO dbo.MoneyTransactions([Id], [WalletId], [Amount_Whole_Part], [Amount_Penny_Part], [Amount], [Currency], [TransactionType], [TransactionDate]) VALUES (@Id, @WalletId, @Amount_Whole_Part, @Amount_Penny_Part, @Amount, @Currency, @TransactionType, @TransactionDate)";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", transactionId, DbType.Guid);
            parameters.Add("WalletId", walletId.Value, DbType.Guid);
            parameters.Add("Amount", amount.Amount.Value, DbType.Decimal);
            parameters.Add("Amount_Whole_Part", amount.Amount.WholePart, DbType.Int32);
            parameters.Add("Amount_Penny_Part", amount.Amount.PennyPart, DbType.Int32);
            parameters.Add("Currency", amount.Currency.CurrencyCode, DbType.String);
            parameters.Add("TransactionType", MoneyTransactionType.Deposit, DbType.Int32);
            parameters.Add("TransactionDate", DateTime.Now, DbType.DateTime);

            return await sqlConnection.ExecuteAsync(query, parameters);
        });

        return transactionId;
    }

    public async Task<Guid> Withdraw(WalletId walletId, MoneyAmountWithCurrency amount)
    {
        Guid transactionId = Guid.NewGuid();
        await _dapperContext.RunWithoutTransactionAsync(async (sqlConnection) =>
        {
            string query = "INSERT INTO dbo.MoneyTransactions([Id], [WalletId], [Amount_Whole_Part], [Amount_Penny_Part], [Amount], [Currency], [TransactionType], [TransactionDate]) VALUES (@Id, @WalletId, @Amount_Whole_Part, @Amount_Penny_Part, @Amount, @Currency, @TransactionType, @TransactionDate)";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", transactionId, DbType.Guid);
            parameters.Add("WalletId", walletId.Value, DbType.Guid);
            parameters.Add("Amount", amount.Amount.Value, DbType.Decimal);
            parameters.Add("Amount_Whole_Part", amount.Amount.WholePart, DbType.Int32);
            parameters.Add("Amount_Penny_Part", amount.Amount.PennyPart, DbType.Int32);
            parameters.Add("Currency", amount.Currency.CurrencyCode, DbType.String);
            parameters.Add("TransactionType", MoneyTransactionType.Withdraw, DbType.Int32);
            parameters.Add("TransactionDate", DateTime.Now, DbType.DateTime);

            return await sqlConnection.ExecuteAsync(query, parameters);
        });

        return transactionId;
    }

    /// <summary>
    /// When fetching data over database, database model and domain model may not reflect same properties.
    /// This type is only used when fetching data with dapper. It can not be used outside of parent DapperWalletRepository type.
    /// </summary>
    private class WalletRawDbData
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
    }

    /// <summary>
    /// When fetching data over database, database model and domain model may not reflect same properties.
    /// This type is only used when fetching data with dapper. It can not be used outside of parent DapperWalletRepository type.
    /// </summary>
    private class MoneyTransactionRawDbData
    {
        public Guid Id { get; set; }

        public Guid WalletId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Amount { get; set; }

        public int Amount_Whole_Part { get; set; }

        public int Amount_Penny_Part { get; set; }

        public string Currency { get; set; } = null!;

        public MoneyTransactionType TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }
    }

    private class MoneyTransactionLogs
    {
        public Guid Id { get; set; }

        public Guid WalletId { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}