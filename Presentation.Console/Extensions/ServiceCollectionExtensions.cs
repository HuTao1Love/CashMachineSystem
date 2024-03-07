using Microsoft.Extensions.DependencyInjection;
using Presentation.Console.Scenarios.Manipulation;
using Presentation.Console.Scenarios.RegisterAndLogin;

namespace Presentation.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, CreateUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LoginScenarioProvider>();

        collection.AddScoped<IScenarioProvider, CreateAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, SelectAccountScenarioProvider>();

        collection.AddScoped<IScenarioProvider, WatchBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, TopUpAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WatchHistoryScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawFromAccountScenarioProvider>();

        collection.AddScoped<IScenarioProvider, BackToChoosingAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LogOutScenarioProvider>();

        return collection;
    }
}