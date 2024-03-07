namespace Application.Abstractions;

public interface IRepository<in TClassInitializer, TClass>
{
    Task<TClass?> Find(long id);
    Task<TClass> Create(TClassInitializer value);
    Task Delete(long id);
}