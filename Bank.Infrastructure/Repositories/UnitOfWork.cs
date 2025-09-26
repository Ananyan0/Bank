using Bank.Domain.Interfaces.IRepositories;
using Bank.Infrastructure.Data;

namespace Bank.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<IAccountRepository> _accountRepository;
        private readonly Lazy<ITransactionRepository> _transactionRepository;
        private readonly Lazy<ICustomerProfileRepository> _customerprofileRepository;
        private readonly Lazy<IBranchRepository> _branchRepository;
        private readonly Lazy<ICustomerBranchRepository> _customerBranchRepository;
        private readonly Lazy<IDirectorRepository> _directorRepository;


        public UnitOfWork(AppDbContext context, Lazy<IDirectorRepository> directorRepository, Lazy<ICustomerRepository> customerRepository, Lazy<IAccountRepository> accountRepository, Lazy<ITransactionRepository> transactionRepository, Lazy<ICustomerProfileRepository> customerprofileRepository, Lazy<IBranchRepository> branchRepository, Lazy<ICustomerBranchRepository> customerBranchRepository)
        {
            _context = context;

            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _customerprofileRepository = customerprofileRepository;
            _branchRepository = branchRepository;
            _customerBranchRepository = customerBranchRepository;
            _directorRepository = directorRepository;
        }

        public ICustomerRepository Customers => _customerRepository.Value;
        public IAccountRepository Accounts => _accountRepository.Value; 
        public ITransactionRepository Transactions => _transactionRepository.Value;
        public ICustomerProfileRepository CustomerProfiles => _customerprofileRepository.Value;
        public IBranchRepository Branches => _branchRepository.Value;
        public ICustomerBranchRepository CustomerBranches => _customerBranchRepository.Value;
        public IDirectorRepository Directors => _directorRepository.Value;


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> CompleteAsync() 
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
