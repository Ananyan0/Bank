namespace Bank.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("Branch")]
public class Branch : EntityBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } = null!;

    // Many-to-many relationship: Branch ↔ Customer
    public ICollection<CustomerBranch> CustomerBranches { get; set; } = new List<CustomerBranch>();
}