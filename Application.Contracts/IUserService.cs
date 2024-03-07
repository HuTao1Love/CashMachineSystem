using Application.Models;

namespace Application.Contracts;

public interface IUserService
{
    Task<User> CreateUser(string login, string password);
    Task<LoginResult> Login(string username, string password);
    Task<bool> CheckLogin(string username);
}