using Spectre.Console;

namespace Presentation.Console;

public class ScenarioRunner(IEnumerable<IScenarioProvider> providers)
{
    private readonly List<IScenarioProvider> _providers = providers.ToList();

    public async Task Run()
    {
        SelectionPrompt<IScenario> selector = new SelectionPrompt<IScenario>()
            .Title("Select action")
            .AddChoices(await GetScenarios())
            .UseConverter(x => x.Name);

        IScenario scenario = AnsiConsole.Prompt(selector);
        string? returned = await scenario.Run();

        if (returned is null)
            return;

        AnsiConsole.WriteLine(returned);
        AnsiConsole.WriteLine("Click any button to continue");
        AnsiConsole.Console.Input.ReadKey(false);
        AnsiConsole.Clear();
    }

    private Task<IEnumerable<IScenario>> GetScenarios()
    {
        IEnumerable<IScenario> values = _providers
            .Select(async i => await i.TryGetScenario())
            .Select(i => i.Result)
            .Where(i => i is not null)
            .Cast<IScenario>();

        return Task.FromResult(values);
    }
}