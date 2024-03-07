namespace Application.Models;

public record Operation
{
    public Operation(long id, OperationInitializer operationInitializer)
    {
        ArgumentNullException.ThrowIfNull(operationInitializer);

        Id = id;
        DateTime = operationInitializer.DateTime;
        AccountId = operationInitializer.AccountId;
        Money = operationInitializer.Money;
    }

    public Operation(long id, DateTime dateTime, long accountId, long money)
    {
        Id = id;
        DateTime = dateTime;
        AccountId = accountId;
        Money = money;
    }

    public long Id { get; }
    public DateTime DateTime { get; }
    public long AccountId { get; }
    public long Money { get; }

    public override string ToString()
    {
        return $"{(Money >= 0 ? '+' : null)}{Money}$ at {DateTime}";
    }
}