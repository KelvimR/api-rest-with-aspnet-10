using api_rest_with_aspnet_10.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace api_rest_with_aspnet_10.Models;

[Table("users")]
public class User : BaseEntity
{
    [Column("user_name")]
    public string Username { get; set; } = string.Empty;

    [Column("full_name")]
    public string Fullname { get; set; } = string.Empty;

    [Column("password")]
    public string Password { get; set; } = string.Empty;

    [Column("refresh_token")]
    public string? RefreshToken { get; set; }

    [Column("refresh_token_expiry_time")]
    public DateTime? RefreshTokenExpiryTime { get; set; }

}
