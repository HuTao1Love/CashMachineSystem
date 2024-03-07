using System.Text;
using Application.Contracts;
using Application.Models;

namespace Presentation.Console.Scenarios.Manipulation;

public class WatchHistoryScenario(IContextService service, IAccountService accountService) : IScenario
{
    public string Name => $"Watch operation history (last {WatchHistoryScenario.MaxHistoryOperationCount} operations)";
    private static long MaxHistoryOperationCount => 10;

    public async Task<string?> Run()
    {
        // this not null because scenarioProvider either don't return this scenario
        if (service.User is null || service.Account is null)
        {
            return null;
        }

        IList<Operation> operations = await accountService.GetOperations(service.Account);
        IEnumerable<Operation> needOperations = operations
            .Skip(int.Max(0, (int)(operations.Count - MaxHistoryOperationCount)))
            .Reverse();

        var stringBuilder = new StringBuilder();

        foreach (Operation operation in needOperations)
        {
            stringBuilder.AppendLine(operation.ToString());
        }

        return stringBuilder.ToString();
    }
}