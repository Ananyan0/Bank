using Bank.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Data;

public class AppDbContext : DbContext
{

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<CustomerProfile> CustomerProfiles => Set<CustomerProfile>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<CustomerBranch> CustomerBranches => Set<CustomerBranch>();

    public DbSet<Director> Directors => Set<Director>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Customer → Accounts (1:M)
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Accounts)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Account → Transactions (1:M)
        modelBuilder.Entity<Account>()
            .HasMany(a => a.Transactions)
            .WithOne(t => t.Account)
            .HasForeignKey(t => t.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        // Transaction → TargetAccount (optional, для переводов)
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.TargetAccount)
            .WithMany() // Если не нужен список входящих переводов, можно оставить пустым
            .HasForeignKey(t => t.TargetAccountId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<CustomerProfile>()
            .HasOne(p => p.Customer)
            .WithOne(c => c.Profile)
            .HasForeignKey<CustomerProfile>(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CustomerBranch>()
            .HasKey(cb => new { cb.CustomerId, cb.BranchId });

        modelBuilder.Entity<CustomerBranch>()
            .HasOne(cb => cb.Customer)
            .WithMany(c => c.CustomerBranches)
            .HasForeignKey(cb => cb.CustomerId);

        modelBuilder.Entity<CustomerBranch>()
            .HasOne(cb => cb.Branch)
            .WithMany(b => b.CustomerBranches)
            .HasForeignKey(cb => cb.BranchId);

        // Ограничение обязательных полей (для надёжности)
        modelBuilder.Entity<Transaction>()
            .Property(t => t.Type)
            .IsRequired();

        modelBuilder.Entity<Customer>()
            .Property(c => c.FirstName)
            .IsRequired();

        modelBuilder.Entity<Account>()
            .Property(a => a.AccountNumber)
            .IsRequired();
    }
}
