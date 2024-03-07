using Application.Abstractions;
using Application.Models;
using Itmo.Dev.Platform.Postgres.Connection;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class OperationRepository(IPostgresConnectionProvider connectionProvider) : IOperationRepository
{
    public async Task<long> GetAvailableId()
    {
        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();

        command.CommandText = """
                              SELECT id
                              FROM operations
                              ORDER BY id DESC
                              LIMIT 1
                              """;

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return 1;
        }

        return reader.GetInt64(0) + 1;
    }

    public async Task<Operation?> Find(long id)
    {
        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM operations WHERE id = @id";
        command.Parameters.AddWithValue("id", id);
        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new Operation(
            reader.GetInt64(0),
            reader.GetDateTime(1),
            reader.GetInt64(2),
            reader.GetInt64(3));
    }

    public async Task<Operation> Create(OperationInitializer value)
    {
        ArgumentNullException.ThrowIfNull(value);
        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();

        command.CommandText = """
                              INSERT INTO operations (date_time, account_id, money) 
                              VALUES (@date_time, @account_id, @money)
                              RETURNING id
                              """;

        command.Parameters.AddWithValue("date_time", value.DateTime);
        command.Parameters.AddWithValue("account_id", value.AccountId);
        command.Parameters.AddWithValue("money", value.Money);
        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        await reader.ReadAsync();

        return new Operation(reader.GetInt64(0), value);
    }

    public async Task Delete(long id)
    {
        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();

        command.CommandText = "DELETE FROM operations WHERE id = @id";

        command.Parameters.AddWithValue("id", id);
        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
    }

    public async Task<IEnumerable<Operation>> GetByAccountId(long accountId)
    {
        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();

        command.CommandText = "SELECT * FROM operations WHERE account_id = @account_id";

        command.Parameters.AddWithValue("account_id", accountId);
        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        var operations = new List<Operation>();
        while (await reader.ReadAsync())
        {
            operations.Add(new Operation(
                reader.GetInt64(0),
                reader.GetDateTime(1),
                reader.GetInt64(2),
                reader.GetInt64(3)));
        }

        return operations;
    }
}