using Application.Abstractions;
using Application.Models;
using Itmo.Dev.Platform.Postgres.Connection;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class UserRepository(IPostgresConnectionProvider connectionProvider) : IUserRepository
{
    public async Task<User?> Find(long id)
    {
        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();

        command.CommandText = """
                              SELECT id, login, password, is_admin
                              FROM users 
                              WHERE id = @id
                              """;

        command.Parameters.AddWithValue("id", id);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new User(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetBoolean(3));
    }

    public async Task<User> Create(UserInitializer value)
    {
        ArgumentNullException.ThrowIfNull(value);

        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();

        command.CommandText = """
                              INSERT INTO users (login, password, is_admin)
                              VALUES (@login, @password, @is_admin)
                              RETURNING id
                              """;

        command.Parameters.AddWithValue("login", value.Login);
        command.Parameters.AddWithValue("password", value.Password);
        command.Parameters.AddWithValue("is_admin", value.IsAdmin);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        await reader.ReadAsync();

        return new User(reader.GetInt64(0), value);
    }

    public async Task Delete(long id)
    {
        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();

        command.CommandText = """
                              DELETE FROM users 
                              WHERE id = @id
                              """;

        command.Parameters.AddWithValue("id", id);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
    }

    public async Task<User?> FindByUsername(string username)
    {
        ArgumentNullException.ThrowIfNull(username);

        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();

        command.CommandText = """
                              SELECT id, login, password, is_admin
                              FROM users 
                              WHERE login = @login
                              """;

        command.Parameters.AddWithValue("login", username);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new User(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetBoolean(3));
    }

    public async Task<bool> CheckLogin(string login)
    {
        ArgumentNullException.ThrowIfNull(login);

        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(CancellationToken.None);
        NpgsqlCommand command = connection.CreateCommand();

        command.CommandText = """
                              SELECT id
                              FROM users
                              WHERE login = @login
                              """;

        command.Parameters.AddWithValue("login", login);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        return !await reader.ReadAsync();
    }
}