using Bank.Domain.Entities;

namespace Bank.Domain.Interfaces.IRepositories;

public interface IRepository<T> where T : EntityBase
{
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task UpdateAsync(T entity);
}
