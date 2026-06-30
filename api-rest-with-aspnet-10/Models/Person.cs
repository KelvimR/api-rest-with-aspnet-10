using api_rest_with_aspnet_10.Models.Base;
using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace api_rest_with_aspnet_10.Models;

[Table("person")]
public class Person : BaseEntity
{
    [Required]
    [Column("first_name", TypeName = "varchar(80)")]
    [MaxLength(80)]
    public string FirstName { get; set; }

    [Required]
    [Column("last_name", TypeName = "varchar(80)")]
    [MaxLength(80)]
    public string LastName { get; set; }

    [Required]
    [Column("address", TypeName = "varchar(100)")]
    [MaxLength(100)]
    public string Address { get; set; }

    [Required]
    [Column("gender", TypeName = "varchar(6)")]
    [MaxLength(6)]
    public string Gender { get; set; }

    [Column("enabled")]
    public bool Enabled { get; set; }

    //[NotMapped] // Indica que não sera mapeado para o banco de dados
    //public DateTime? Birthday { get; set; }
}
