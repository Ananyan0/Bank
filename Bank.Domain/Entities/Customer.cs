using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Bank.Domain.Entities;

[Table("Customer")]
public class Customer : EntityBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Column(TypeName = "varchar(20)")]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "varchar(150)")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public List<Account> Accounts { get; set; } = new();

    public ICollection<CustomerBranch> CustomerBranches { get; set; } = new List<CustomerBranch>();

    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = "User";



    [JsonIgnore] 
    public CustomerProfile? Profile { get; set; }
}
