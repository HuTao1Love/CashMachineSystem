using Application.Models;

namespace Application.Abstractions;

public interface IUserRepository : IRepository<UserInitializer, User>
{
    Task<User?> FindByUsername(string username);
    Task<bool> CheckLogin(string login);
}