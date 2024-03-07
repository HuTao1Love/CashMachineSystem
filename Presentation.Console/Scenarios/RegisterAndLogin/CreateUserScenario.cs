using Application.Contracts;
using Spectre.Console;

namespace Presentation.Console.Scenarios.RegisterAndLogin;

public class CreateUserScenario(IContextService service, IUserService userService) : IScenario
{
    public string Name => "Register new user";

    public async Task<string?> Run()
    {
        string login = AnsiConsole.Ask<string>("Enter your login");

        while (!await userService.CheckLogin(login))
        {
            AnsiConsole.Clear();
            login = AnsiConsole.Ask<string>("Login already used. Enter new login");
        }

        string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter your password").Secret());
        service.User = await userService.CreateUser(login, password);

        return "Successful created user";
    }
}