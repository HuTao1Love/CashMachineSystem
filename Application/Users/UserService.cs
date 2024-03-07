using Application.Abstractions;
using Application.Contracts;
using Application.Models;

namespace Application.Users;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<LoginResult> Login(string username, string password)
    {
        User? user = await userRepository.FindByUsername(username);

        if (user == null)
        {
            return new LoginResult.InvalidUsernameOrPassword(false);
        }

        if (user.Password != password)
        {
            return new LoginResult.InvalidUsernameOrPassword(user.IsAdmin);
        }

        if (user.IsAdmin)
        {
            return new LoginResult.Admin();
        }

        return new LoginResult.Success(user);
    }

    public async Task<bool> CheckLogin(string username)
    {
        return await userRepository.CheckLogin(username);
    }

    public async Task<User> CreateUser(string login, string password)
    {
        var user = new UserInitializer(
            login,
            password,
            false);

        return await userRepository.Create(user);
    }
}