using Application.Contracts;

namespace Presentation.Console.Scenarios.RegisterAndLogin;

public class CreateAccountScenario(IContextService contextService, IAccountService accountService) : IScenario
{
    public string Name => "Create new account";

    public async Task<string?> Run()
    {
        // this not null because scenarioProvider either don't return this scenario
        if (contextService.User is null)
        {
            return null;
        }

        contextService.Account = await accountService.CreateAccount(contextService.User);
        return "Successful created account";
    }
}