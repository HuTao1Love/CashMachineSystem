using Application.Contracts;

namespace Presentation.Console.Scenarios.RegisterAndLogin;

public class CreateAccountScenarioProvider(IContextService service, IAccountService accountService) : IScenarioProvider
{
    public Task<IScenario?> TryGetScenario()
    {
        return Task.FromResult<IScenario?>(
            service.User is not null && service.Account is null
                ? new CreateAccountScenario(service, accountService)
                : null);
    }
}