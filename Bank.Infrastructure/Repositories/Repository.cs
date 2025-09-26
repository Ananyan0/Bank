using Bank.Application.Exceptions;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Bank.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;



namespace Bank.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    protected readonly AppDbContext _context;
    private readonly DbSet<T> _entities;

    public Repository(AppDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }


    public async Task<T> GetByIdAsync(int id) 
    {
        var branch = await _entities.FindAsync(id);
        if (branch == null)
            throw new BranchException($"There is no branch with id -> {id}");

        return branch;
    }

    public async Task<List<T>> GetAllAsync()
    {
        var branch = await _entities.ToListAsync();
        if(branch == null)
            throw new BranchException("No branches found");

        return branch;
    }
    
    public async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _entities.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity; 
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



    public async Task<List<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _entities;

        foreach (var include in includes)
            query = query.Include(include);

        return await query.ToListAsync();
    }

    public IQueryable<T> Query() => _entities.AsQueryable();



    public async Task<Customer?> GetWithProfileAsync(int id)
    {
        return await _context.Customers
            .Include(c => c.Profile)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _entities.FirstOrDefaultAsync(predicate);
    }

    public async Task RemoveRangeAsync(IEnumerable<Customer> customers)
    {
        if (customers == null || !customers.Any()) return;

        await Task.Run(() =>
        {
            _context.Customers.RemoveRange(customers);
        });
    }

}
