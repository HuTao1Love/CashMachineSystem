using Application.Contracts;
using Spectre.Console;

namespace Presentation.Console.Scenarios.Manipulation;

public class TopUpAccountScenario(IContextService service, IAccountService accountService) : IScenario
{
    public string Name => "Top up account";

    public async Task<string?> Run()
    {
        // this not null because scenarioProvider either don't return this scenario
        if (service.User is null || service.Account is null)
        {
            return null;
        }

        long money = AnsiConsole.Ask<long>($"how much do you want to top up <emulate inserting banknotes>?");

        OperationResult result = await accountService.TopUp(service.Account, money);
        return result.Message;
    }
}