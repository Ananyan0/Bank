using Bank.Domain.Entities;

namespace Bank.Domain.Interfaces.IRepositories;


    public interface IAccountRepository
    {
        Task AddAsync(Account account);
        Task<List<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(int id);
        Task DeleteAsync(Account account);


}

