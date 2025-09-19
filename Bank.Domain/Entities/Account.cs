using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Bank.Domain.Entities;

[Table("Account")]
public class Account : EntityBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string AccountNumber { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "nvarchar(100)")]

    public string AccountName { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; }

    // Foreign key to Customer
    [Required]
    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    [JsonIgnore] // prevent serialization cycles
    public Customer Customer { get; set; } = null!;

    // One-to-many: Account -> Transactions
    [JsonIgnore] // prevent cycles in JSON
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
