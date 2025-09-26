using Bank.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Application.DTOs.ResponseDTOs;

public class CustomerWithAccountsResponse
{
    
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public string Email { get; set; } = string.Empty;
    public List<Account> Accounts { get; set; } = new();

}
