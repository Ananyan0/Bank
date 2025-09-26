using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Bank.Infrastructure.Data;

namespace Bank.Infrastructure.Repositories;

public class DirectorRepository : Repository<Director>, IDirectorRepository
{

    public DirectorRepository(AppDbContext context) : base(context)
    {
    }
}
