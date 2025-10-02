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
    public string Type { get; set; } = null!; 

    [Required]
    public int AccountId { get; set; }

    [ForeignKey("AccountId")]
    [JsonIgnore] 
    public Account Account { get; set; } = null!;

    public int? TargetAccountId { get; set; }

    [ForeignKey("TargetAccountId")]
    [JsonIgnore]
    public Account? TargetAccount { get; set; }
}

