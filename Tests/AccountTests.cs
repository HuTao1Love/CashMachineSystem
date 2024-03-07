using Application.Abstractions;
using Application.Contracts;
using Application.Models;
using Application.Users;
using Moq;

namespace Tests;

public class AccountTests
{
    private readonly IAccountService _service;

    private readonly Account _account;

    public AccountTests()
    {
        // ARRANGE
        IAccountRepository accountRepository = Mock.Of<IAccountRepository>();
        IOperationRepository operationRepository = Mock.Of<IOperationRepository>();
        _account = new Account(1, 1, 500);
        _service = new AccountService(accountRepository, operationRepository);
    }

    [Fact]
    public async Task AddMoneyTest()
    {
        // ACT
        OperationResult result = await _service.TopUp(_account, 500);
        var success = result as OperationResult.Success;

        // ASSERT
        Assert.NotNull(success);
        Assert.Equal(1000, success.CurrentMoney);
    }

    [Fact]
    public async Task RemoveMoneySuccessTest()
    {
        // ACT
        OperationResult result = await _service.Withdraw(_account, 500);
        var success = result as OperationResult.Success;

        // ASSERT
        Assert.NotNull(success);
        Assert.Equal(0, success.CurrentMoney);
    }

    [Fact]
    public async Task RemoveMoneyInvalidTest()
    {
        // ACT
        OperationResult result = await _service.Withdraw(_account, 1000);
        var fail = result as OperationResult.Fail;

        // ASSERT
        Assert.NotNull(fail);
    }
}