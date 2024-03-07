using Application.Models;

namespace Application.Abstractions;

public interface IOperationRepository : IRepository<OperationInitializer, Operation>
{
    Task<IEnumerable<Operation>> GetByAccountId(long accountId);
}