using Application.Models;

namespace Application.Abstractions;

public interface IAccountRepository : IRepository<AccountInitializer, Account>
{
    Task<IEnumerable<Account>> GetByUserId(long userId);
    Task UpdateMoney(Account account);
}