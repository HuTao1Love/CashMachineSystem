using Application.Contracts;

namespace Presentation.Console.Scenarios.RegisterAndLogin;

public class LoginScenarioProvider(IContextService service, IUserService userService) : IScenarioProvider
{
    public Task<IScenario?> TryGetScenario()
    {
        return Task.FromResult<IScenario?>(
            service.User is null
                ? new LoginScenario(userService, service)
                : null);
    }
}