using Application.Contracts;
using Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IContextService, ContextService>();
        collection.AddScoped<IAccountService, AccountService>();
        return collection;
    }
}