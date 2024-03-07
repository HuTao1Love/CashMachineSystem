using Application.Models;

namespace Application.Contracts;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetUserAccounts(User user);
    Task<Account> CreateAccount(User owner);
    Task<OperationResult> TopUp(Account account, long money);
    Task<OperationResult> Withdraw(Account account, long money);
    Task<IList<Operation>> GetOperations(Account account);
}