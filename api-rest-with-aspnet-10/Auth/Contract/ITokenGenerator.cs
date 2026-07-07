using api_rest_with_aspnet_10.Data.DTO.V1;
using api_rest_with_aspnet_10.Models;
using System.Globalization;
using System.Security.Claims;

namespace api_rest_with_aspnet_10.Auth.Contract;

public interface ITokenGenerator
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    TokenDTO GenerateToken(User user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
