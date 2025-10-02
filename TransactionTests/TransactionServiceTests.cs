using Bank.Application.DTOs.CreateDTOs;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Moq;
using Xunit;

namespace TransactionTests;

public class TransactionServiceTests
{
    [Fact]
    public async Task Deposit_ShouldIncreaseBalance()
    {
        // Arrange: create a fake account
        var account = new Account { Id = 1, Balance = 100 };

        // Arrange: mock IUnitOfWork and Accounts repository
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.Accounts.GetByIdAsync(1))
                      .ReturnsAsync(account);
        mockUnitOfWork.Setup(u => u.SaveChangesAsync())
                      .ReturnsAsync(1); // or ReturnsAsync(1) if method returns Task<int>

        // Create service with mocked dependencies
        var service = new TransactionService(mockUnitOfWork.Object);

        // Prepare deposit request
        var request = new CreateTransactionRequest
        {
            AccountId = 1,
            Amount = 50
        };

        // Act: perform deposit
        var response = await service.DepositAsync(request);

        // Assert: check balance and response
        Assert.Equal(150, account.Balance);
        Assert.NotNull(response);

    }
}
