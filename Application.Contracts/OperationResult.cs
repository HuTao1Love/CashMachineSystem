namespace Application.Contracts;

public abstract record OperationResult
{
    private OperationResult(string message)
    {
        Message = message;
    }

    public string Message { get; }

    public sealed record Success(long CurrentMoney) : OperationResult($"Success. Current balance: {CurrentMoney}$")
    {
        public long CurrentMoney { get; init; } = CurrentMoney;
    }

    public sealed record Fail(string Message) : OperationResult(Message);
}