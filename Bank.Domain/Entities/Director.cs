using System.ComponentModel.DataAnnotations;

namespace Bank.Domain.Entities
{
    public class Director : EntityBase
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be exactly 9 digits.")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public int BranchId { get; set; }  


        public Branch Branch { get; set; } = null!;
    }
}
