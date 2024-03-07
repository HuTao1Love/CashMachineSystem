using Application.Contracts;

namespace Presentation.Console.Scenarios.Manipulation;

public class WatchBalanceScenario(IContextService service) : IScenario
{
    public string Name => "Watch current balance";
    public Task<string?> Run()
    {
        // this not null because scenarioProvider either don't return this scenario
        if (service.User is null || service.Account is null)
        {
            return Task.FromResult<string?>(null);
        }

        return Task.FromResult<string?>($"Current balance: {service.Account.Balance}");
    }
}