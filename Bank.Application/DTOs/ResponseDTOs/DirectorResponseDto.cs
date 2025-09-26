using Bank.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Bank.Application.DTOs.ResponseDTOs
{
    public class DirectorResponseDto
    {


        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int BranchId { get; set; }
    }
}
