using Application.Abstractions;
using Application.Contracts;
using Application.Models;

namespace Application.Users;

public class ContextService(IUserRepository userRepository, IAccountRepository accountRepository)
    : IContextService
{
    public User? User { get; set; }
    public Account? Account { get; set; }

    public async Task SetUserById(long id)
    {
        User = await userRepository.Find(id);
    }

    public async Task SetAccountById(long id)
    {
        Account = await accountRepository.Find(id);
    }
}