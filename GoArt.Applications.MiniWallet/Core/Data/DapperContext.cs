using System.Transactions;
using GoArt.Applications.MiniWallet.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace GoArt.Applications.MiniWallet.Core.Data;

public sealed class DapperContext
{
    private readonly IConfiguration _configuration;

    private readonly string? _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("GoArtConnection");
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new ArgumentNullException(nameof(_connectionString));
        }
    }

    public SqlConnection CreateConnection()
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        return connection;
    }

    public async Task<T> RunWithoutTransactionAsync<T>(Func<SqlConnection, Task<T>> action)
    {
        T? response = default(T);
        using (SqlConnection connection = CreateConnection())
        {
            response = await action(connection);
        }
        return response;
    }

    public async Task RunWithoutTransactionAsync(Func<SqlConnection, Task> action)
    {
        using (SqlConnection connection = CreateConnection())
        {
            await action(connection);
        }
    }

    public async Task RunWithTransaction(Func<SqlConnection, Task> action)
    {
        using (TransactionScope transaction = new TransactionScope())
        {
            using (SqlConnection connection = CreateConnection())
            {
                await action(connection);
                transaction.Complete();
            }
        }
    }
}