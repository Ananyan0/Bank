using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Bank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _entities;

    public Repository(AppDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id) =>
        await _entities.FindAsync(id);

    public async Task<List<T>> GetAllAsync() =>
        await _entities.ToListAsync();

    public async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _entities.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _entities.Attach(entity);
        }
        _entities.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
