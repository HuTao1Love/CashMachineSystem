using Application.Contracts;

namespace Presentation.Console.Scenarios.RegisterAndLogin;

public class LogOutScenario(IContextService service) : IScenario
{
    public string Name => "Log out";

    public Task<string?> Run()
    {
        service.Account = null;
        service.User = null;
        return Task.FromResult<string?>(null);
    }
}