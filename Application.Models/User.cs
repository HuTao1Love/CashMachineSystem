namespace Application.Models;

public record User
{
    public User(long id, UserInitializer userInitializer)
    {
        ArgumentNullException.ThrowIfNull(userInitializer);

        Id = id;
        Login = userInitializer.Login;
        Password = userInitializer.Password;
        IsAdmin = userInitializer.IsAdmin;
    }

    public User(long id, string login, string password, bool isAdmin)
    {
        Id = id;
        Login = login;
        Password = password;
        IsAdmin = isAdmin;
    }

    public long Id { get; }
    public string Login { get; }
    public string Password { get; }
    public bool IsAdmin { get; }
}