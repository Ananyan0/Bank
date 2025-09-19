# 🏦 Bank System

Bank System is a learning project built with **.NET 8 / ASP.NET Core Web API**, following the principles of **Onion Architecture** and using **Entity Framework Core**.

## 📂 Solution Structure

The solution is divided into 4 layers:

### 1. Bank.Domain
Domain layer:
- **Entities**  
  - `Customer`, `Account`, `Transaction`, `CustomerProfile`, `Branch`, `CustomerBranch`  
- **EntityBase** – base class for all entities (Id, CreatedAt, etc.)
- **Interfaces/Repositories**  
  - Repository contracts (`ICustomerRepository`, `IAccountRepository`, `IBranchRepository`, `ICustomerBranchRepository`, etc.)
  - `IUnitOfWork` – unit of work contract

### 2. Bank.Application
Business logic layer:
- **DTOs** – request and response models (`CreateDTOs`, `UpdateDTOs`, `ResponseDTOs`)
- **Interfaces** – service contracts (`ICustomerService`, `ITransactionService`, etc.)
- **Services** – implementation of business logic working via UnitOfWork

### 3. Bank.Infrastructure
Data access layer:
- **Data** – `AppDbContext` (EF Core DbContext)
- **Repositories** – repository implementations (`CustomerRepository`, `BranchRepository`, `CustomerBranchRepository`, `TransactionRepository`, etc.)
- **Migrations** – EF Core database migrations

### 4. Bank.API
Presentation layer:
- **Controllers** – REST API controllers (`CustomerController`, `AccountController`, `TransactionController`, `CustomerProfileController`, etc.)
- Application configuration (Dependency Injection, Swagger, etc.)

---

## ⚙️ Technologies
- **.NET 8** / ASP.NET Core Web API  
- **Entity Framework Core**  
- **InMemory / SQL Server provider** (depending on configuration)  
- **Swagger (Swashbuckle)** for API documentation  
- **Dependency Injection** (via .NET built-in container)  

---

## 🚀 Getting Started
1. Clone the repository:
   ```bash
   git clone https://github.com/Ananyan0/Bank.git
   cd Bank
