using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Bank.Domain.Entities;

[Table("Customer")] // Optional: specify table name
public class Customer : EntityBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "varchar(20)")]
    [Phone]
    public string? PhoneNumber { get; set; }

    [Required]
    [Column(TypeName = "varchar(150)")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public List<Account> Accounts { get; set; } = new();

    public ICollection<CustomerBranch> CustomerBranches { get; set; } = new List<CustomerBranch>();

    
    [JsonIgnore] 
    public CustomerProfile? Profile { get; set; }
}
