using Application.Abstractions;
using Application.Models;
using Itmo.Dev.Platform.Postgres.Connection;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class AccountRepository(IPostgresConnectionProvider connectionProvider) : IAccountRepository
{
    public async Task<Account?> Find(long id)
    {
        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = """
                              SELECT id, user_id, balance
                              FROM accounts
                              WHERE id = @id
                              """;
        command.Parameters.AddWithValue("id", id);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new Account(
            reader.GetInt64(0),
            reader.GetInt64(1),
            reader.GetInt64(2));
    }

    public async Task Delete(long id)
    {
        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM accounts WHERE id = @id";
        command.Parameters.AddWithValue("id", id);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
    }

    public async Task<IEnumerable<Account>> GetByUserId(long userId)
    {
        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();

        command.CommandText = """
                              SELECT id, user_id, balance
                              FROM accounts
                              WHERE user_id  = @user_id
                              """;

        command.Parameters.AddWithValue("user_id", userId);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        var accounts = new List<Account>();
        while (await reader.ReadAsync())
        {
            accounts.Add(new Account(
                reader.GetInt64(0),
                new AccountInitializer(
                    reader.GetInt64(1),
                    reader.GetInt64(2))));
        }

        return accounts;
    }

    public async Task UpdateMoney(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);

        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();

        command.CommandText = """
                              UPDATE accounts 
                              SET balance = @balance
                              WHERE Id = @id
                              """;

        command.Parameters.AddWithValue("id", account.Id);
        command.Parameters.AddWithValue("balance", account.Balance);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
    }

    public async Task<Account> Create(AccountInitializer value)
    {
        ArgumentNullException.ThrowIfNull(value);

        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();

        command.CommandText = """
                              INSERT INTO accounts (user_id, balance) 
                              VALUES (@user_id, @balance)
                              RETURNING id
                              """;

        command.Parameters.AddWithValue("user_id", value.UserId);
        command.Parameters.AddWithValue("balance", value.Balance);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        await reader.ReadAsync();

        return new Account(reader.GetInt64(0), value);
    }
}