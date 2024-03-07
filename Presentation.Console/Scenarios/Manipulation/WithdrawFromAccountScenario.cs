using Application.Contracts;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Manipulation;

public class WithdrawFromAccountScenario(IContextService service, IAccountService accountService) : IScenario
{
    public string Name => "Withdraw from account";

    public async Task<string?> Run()
    {
        // this not null because scenarioProvider either don't return this scenario
        if (service.User is null || service.Account is null)
        {
            return null;
        }

        long money = AnsiConsole.Ask<long>($"How much do you want to withdraw?");

        OperationResult result = await accountService.Withdraw(service.Account, money);
        return result.Message;
    }
}