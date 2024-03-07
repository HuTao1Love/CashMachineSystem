using Application.Contracts;

namespace Presentation.Console.Scenarios.RegisterAndLogin;

public class BackToChoosingAccountScenarioProvider(IContextService service) : IScenarioProvider
{
    public Task<IScenario?> TryGetScenario()
    {
        return Task.FromResult<IScenario?>(
            service.User is not null && service.Account is not null
                ? new BackToChoosingAccountScenario(service)
                : null);
    }
}