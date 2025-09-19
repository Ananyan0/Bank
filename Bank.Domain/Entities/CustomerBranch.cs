using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Domain.Entities;

[Table("CustomerBranch")]
public class CustomerBranch : EntityBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } // Optional: EF Core 6+ allows composite keys without Id, but having Id is simpler

    // Foreign key to Customer
    [Required]
    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; } = null!;

    // Foreign key to Branch
    [Required]
    public int BranchId { get; set; }

    [ForeignKey("BranchId")]
    public Branch Branch { get; set; } = null!;
}
