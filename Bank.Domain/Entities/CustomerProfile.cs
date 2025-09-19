using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Domain.Entities;

[Table("CustomerProfile")]
public class CustomerProfile : EntityBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public string Address { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string PassportNumber { get; set; } = null!;

    [Required]
    public DateTime DateOfBirth { get; set; }

    // Foreign key to Customer
    [Required]
    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; } = null!;
}