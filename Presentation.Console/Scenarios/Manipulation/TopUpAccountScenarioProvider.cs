using Application.Contracts;

namespace Presentation.Console.Scenarios.Manipulation;

public class TopUpAccountScenarioProvider(IContextService service, IAccountService accountService) : IScenarioProvider
{
    public Task<IScenario?> TryGetScenario()
    {
        return Task.FromResult<IScenario?>(
            service.User is not null && service.Account is not null
                ? new TopUpAccountScenario(service, accountService)
                : null);
    }
}