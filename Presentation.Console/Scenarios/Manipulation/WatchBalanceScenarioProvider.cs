using Application.Contracts;

namespace Presentation.Console.Scenarios.Manipulation;

public class WatchBalanceScenarioProvider(IContextService service) : IScenarioProvider
{
    public Task<IScenario?> TryGetScenario()
    {
        return Task.FromResult<IScenario?>(
            service.User is not null && service.Account is not null
                ? new WatchBalanceScenario(service)
                : null);
    }
}