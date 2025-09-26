using System.ComponentModel.DataAnnotations;

namespace Bank.Application.DTOs.CreateDTOs;

public class AssignCustomerToBranchRequest
{
    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int BranchId { get; set; }
}
