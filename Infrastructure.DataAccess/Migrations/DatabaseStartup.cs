using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class DatabaseStartup : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
        => """
           CREATE TABLE IF NOT EXISTS users (
               id BIGSERIAL PRIMARY KEY,
               login TEXT NOT NULL UNIQUE,
               password TEXT NOT NULL,
               is_admin BOOLEAN NOT NULL
           );

           CREATE TABLE IF NOT EXISTS accounts (
               id BIGSERIAL PRIMARY KEY,
               user_id BIGINT NOT NULL REFERENCES users(id),
               balance BIGINT NOT NULL
           );

           CREATE TABLE IF NOT EXISTS operations (
               id BIGSERIAL PRIMARY KEY,
               date_time TIMESTAMP NOT NULL,
               account_id BIGINT NOT NULL REFERENCES accounts(id),
               money BIGINT NOT NULL
           );
           """;

    protected override string GetDownSql(IServiceProvider serviceProvider)
        => """
           DROP TABLE operations;
           DROP TABLE accounts;
           DROP TABLE users;
           """;
}