using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Bank.Domain.Entities;
[Table("Transactions")]
public class Transaction : EntityBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [Column(TypeName = "varchar(20)")]
    public string Type { get; set; } = null!; // Deposit, Withdraw, Transfer

    // Foreign key to Account
    [Required]
    public int AccountId { get; set; }

    [ForeignKey("AccountId")]
    [JsonIgnore] // prevent cycles in JSON serialization
    public Account Account { get; set; } = null!;

    // Optional foreign key to TargetAccount for transfers
    public int? TargetAccountId { get; set; }

    [ForeignKey("TargetAccountId")]
    [JsonIgnore]
    public Account? TargetAccount { get; set; }
}

