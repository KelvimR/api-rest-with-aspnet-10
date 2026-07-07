using api_rest_with_aspnet_10.Auth.Configurations;
using api_rest_with_aspnet_10.Auth.Contract;
using api_rest_with_aspnet_10.Data.DTO.V1;
using api_rest_with_aspnet_10.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace api_rest_with_aspnet_10.Auth.Tools;

public class TokenGenerator(TokenConfiguration configurations) : ITokenGenerator
{
    // The TokenConfiguration instance is injected into the constructor and stored in a private readonly field for use in the methods of this class.
    private readonly TokenConfiguration _configuration = configurations;
    
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(
            issuer: _configuration.Issuer,
            audience: _configuration.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_configuration.Minutes),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public TokenDTO GenerateToken(User user)
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false, // We want to get the claims from an expired token, so we set this to false
            ValidateIssuerSigningKey = true,        
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret))
        };

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var principal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        // Check if the security token is a valid JWT and if it uses the expected signing algorithm (HMAC SHA256). If not, throw a SecurityTokenException.
        if (securityToken is not JwtSecurityToken jwtSecurityToken 
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token.");
        }

        return principal;
    }
}
