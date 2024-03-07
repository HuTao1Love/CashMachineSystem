using Application.Models;

namespace Application.Contracts;

public abstract record LoginResult
{
    private LoginResult(string message)
    {
        Message = message;
    }

    public string Message { get; }

    public sealed record Success(User User) : LoginResult($"Successful login as {User.Login}");

    public sealed record InvalidUsernameOrPassword(bool IsAdmin) : LoginResult($"Invalid login or password");

    public sealed record Admin() : LoginResult($"Successful login as admin");
}