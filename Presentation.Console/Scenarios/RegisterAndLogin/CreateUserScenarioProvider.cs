using Application.Contracts;

namespace Presentation.Console.Scenarios.RegisterAndLogin;

public class CreateUserScenarioProvider(IContextService service, IUserService createUserService) : IScenarioProvider
{
    public Task<IScenario?> TryGetScenario()
    {
        return Task.FromResult<IScenario?>(
            service.User is null
                ? new CreateUserScenario(service, createUserService)
                : null);
    }
}