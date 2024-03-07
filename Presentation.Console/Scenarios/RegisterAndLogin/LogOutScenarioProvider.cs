using Application.Contracts;

namespace Presentation.Console.Scenarios.RegisterAndLogin;

public class LogOutScenarioProvider(IContextService service) : IScenarioProvider
{
    public Task<IScenario?> TryGetScenario()
    {
        return Task.FromResult<IScenario?>(
            service.User is not null
                ? new LogOutScenario(service)
                : null);
    }
}