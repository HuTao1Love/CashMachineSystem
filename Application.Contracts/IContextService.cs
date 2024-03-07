using Application.Models;

namespace Application.Contracts;

public interface IContextService
{
    User? User { get; set; }
    Account? Account { get; set; }

    Task SetUserById(long id);
    Task SetAccountById(long id);
}