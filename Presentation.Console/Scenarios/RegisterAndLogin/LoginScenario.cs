using Application.Contracts;
using Spectre.Console;

namespace Presentation.Console.Scenarios.RegisterAndLogin;

public class LoginScenario(IUserService userService, IContextService contextService) : IScenario
{
    public string Name => "Login";

    public async Task<string?> Run()
    {
        string login = AnsiConsole.Ask<string>("Enter your login");
        string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter your password").Secret());

        LoginResult result = await userService.Login(login, password);
        AnsiConsole.WriteLine(result.Message);

        switch (result)
        {
            case LoginResult.InvalidUsernameOrPassword { IsAdmin: true }:
                Environment.Exit(1);
                break;
            case LoginResult.Success success:
                contextService.User = success.User;
                break;
            case LoginResult.Admin:
            {
                long id = AnsiConsole.Ask<long>("Enter user account id");
                await contextService.SetUserById(id);
                break;
            }
        }

        return null;
    }
}