using Application.Abstractions;
using Application.Contracts;
using Application.Models;

namespace Application.Users;

public class AccountService(IAccountRepository repository, IOperationRepository operationRepository)
    : IAccountService
{
    public async Task<IEnumerable<Account>> GetUserAccounts(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        return await repository.GetByUserId(user.Id);
    }

    public async Task<Account> CreateAccount(User owner)
    {
        ArgumentNullException.ThrowIfNull(owner);

        var account = new AccountInitializer(
            owner.Id,
            0);

        return await repository.Create(account);
    }

    public async Task<OperationResult> TopUp(Account account, long money)
    {
        ArgumentNullException.ThrowIfNull(account);

        if (money < 0)
        {
            return new OperationResult.Fail($"Invalid money amount");
        }

        account.Balance += money;
        await repository.UpdateMoney(account);

        await CreateOperation(account, money);

        return new OperationResult.Success(account.Balance);
    }

    public async Task<OperationResult> Withdraw(Account account, long money)
    {
        ArgumentNullException.ThrowIfNull(account);

        if (money < 0)
        {
            return new OperationResult.Fail($"Invalid money amount");
        }

        if (account.Balance < money)
        {
            return new OperationResult.Fail($"Not enough money");
        }

        account.Balance -= money;
        await repository.UpdateMoney(account);

        await CreateOperation(account, -money);

        return new OperationResult.Success(account.Balance);
    }

    public async Task<IList<Operation>> GetOperations(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);

        return (await operationRepository
            .GetByAccountId(account.Id))
            .ToList();
    }

    private async Task CreateOperation(Account account, long money)
    {
        await operationRepository.Create(
            new OperationInitializer(
                DateTime.Now,
                account.Id,
                money));
    }
}