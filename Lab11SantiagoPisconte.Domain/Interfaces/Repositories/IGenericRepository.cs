using System.Linq.Expressions;

namespace Lab11SantiagoPisconte.Domain.Interfaces.Repositories;

public interface IGenericRepository<T, TKey> where T : class
{
    Task<T?> GetByIdAsync(TKey id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task<IEnumerable<T>> FindWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    void Update(T entity);
    void Delete(T entity);
}