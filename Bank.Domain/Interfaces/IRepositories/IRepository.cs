using Bank.Domain.Entities;
using System.Linq.Expressions;

namespace Bank.Domain.Interfaces.IRepositories;

public interface IRepository<T> where T : EntityBase
{
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);

    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> UpdateAsync(T entity);
    Task<List<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes);
    IQueryable<T> Query();
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);

}
