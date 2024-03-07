namespace Presentation.Console;

public interface IScenarioProvider
{
    Task<IScenario?> TryGetScenario();
}