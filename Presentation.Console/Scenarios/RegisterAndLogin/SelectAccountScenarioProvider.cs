using Application.Contracts;

namespace Presentation.Console.Scenarios.RegisterAndLogin;

public class SelectAccountScenarioProvider(IContextService service, IAccountService accountService) : IScenarioProvider
{
    public async Task<IScenario?> TryGetScenario()
    {
        if (
            service.User is null ||
            service.Account is not null ||
            !(await accountService.GetUserAccounts(service.User)).Any())
        {
            return null;
        }

        return new SelectAccountScenario(service, accountService);
    }
}