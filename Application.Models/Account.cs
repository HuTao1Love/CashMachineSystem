namespace Application.Models;

public class Account
{
    public Account(long id, AccountInitializer accountInitializer)
    {
        ArgumentNullException.ThrowIfNull(accountInitializer);

        Id = id;
        UserId = accountInitializer.UserId;
        Balance = accountInitializer.Balance;
    }

    public Account(long id, long userId, long balance)
    {
        Id = id;
        UserId = userId;
        Balance = balance;
    }

    public long Id { get; }
    public long UserId { get; }
    public long Balance { get; set; }
}