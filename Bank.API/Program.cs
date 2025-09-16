using Bank.Application.Services;
using Bank.Domain.Interfaces.IRepositories;
using Bank.Domain.Interfaces.IServices;
using Bank.Infrastructure.Data;
using Bank.Infrastructure.Helpers;
using Bank.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using IAccountRepository = Bank.Domain.Interfaces.IRepositories.IAccountRepository;
using IAccountService = Bank.Domain.Interfaces.IServices.IAccountService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("BankDB"));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped(typeof(Lazy<>), typeof(LazyService<>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
