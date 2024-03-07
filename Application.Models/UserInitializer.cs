namespace Application.Models;

public record UserInitializer(
    string Login,
    string Password,
    bool IsAdmin);