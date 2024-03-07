using Application.Contracts;
using Application.Models;
using Spectre.Console;

namespace Presentation.Console.Scenarios.RegisterAndLogin;

public class SelectAccountScenario(IContextService service, IAccountService accountService) : IScenario
{
    public string Name => "Select your account";

    public async Task<string?> Run()
    {
        // User is not null, either cannot access scenario
        if (service.User is null)
        {
            return null;
        }

        IEnumerable<Account> accounts = await accountService.GetUserAccounts(service.User);

        SelectionPrompt<Account> selector = new SelectionPrompt<Account>()
            .Title("Select account")
            .AddChoices(accounts)
            .UseConverter(x => $"{x.Balance}$");

        service.Account = AnsiConsole.Prompt(selector);
        return null;
    }
}