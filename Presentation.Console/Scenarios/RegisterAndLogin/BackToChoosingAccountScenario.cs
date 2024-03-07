using Application.Contracts;

namespace Presentation.Console.Scenarios.RegisterAndLogin;

public class BackToChoosingAccountScenario(IContextService service) : IScenario
{
    public string Name => "Back to choosing account";

    public Task<string?> Run()
    {
        service.Account = null;
        return Task.FromResult<string?>(null);
    }
}