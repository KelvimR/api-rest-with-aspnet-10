using api_rest_with_aspnet_10.Auth.Configurations;
using api_rest_with_aspnet_10.Auth.Contract;
using api_rest_with_aspnet_10.Data.DTO.V1;
using api_rest_with_aspnet_10.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace api_rest_with_aspnet_10.Services.Implementations;

public class LoginServiceImpl : ILoginService
{
    private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

    private readonly IUserAuthService _userAuthService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenService;
    private readonly TokenConfiguration _Configuration;

    public LoginServiceImpl(IUserAuthService userAuthService, IPasswordHasher passwordHasher, ITokenGenerator tokenService, TokenConfiguration configuration)
    {
        _userAuthService = userAuthService;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _Configuration = configuration;
    }

    public TokenDTO ValidateCredentials(UserDTO userdto)
    {
        var user = _userAuthService.FindByUserName(userdto.Username);
        if (user == null || !_passwordHasher.Verify(userdto.Password, user.Password))
            return null;

        return GenerateToken(user);
    }

    public TokenDTO ValidateCredentials(TokenDTO token)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(token.AccessToken);

        var username = principal.Identity?.Name;
        var user = _userAuthService.FindByUserName(username);
        if(user == null || user.RefreshToken != token.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) return null;

        return GenerateToken(user, principal.Claims);
    }

    public AccountCredentialsDTO Create(AccountCredentialsDTO dto)
    {
        var usernameExists = _userAuthService.FindByUserName(dto.Username);
        if (usernameExists != null) return null;

        var user = _userAuthService.Create(dto);
        return new AccountCredentialsDTO
        {
            Username = user.Username,
            Fullname = user.Fullname,
            Password = "*********"
        };
    }

    public bool RevokeToken(string username)
    {
        return _userAuthService.RevokeToken(username);
    }

    //Serve para gerar o token de acesso e refresh token para o usuário autenticado
    //ToString("N") serve para gerar um identificador único sem hífens, que é usado como o valor do claim Jti (JWT ID) no token JWT.
    //O claim Jti é usado para identificar de forma única cada token emitido, ajudando a prevenir ataques de repetição (replay attacks) e permitindo que o servidor rastreie tokens individuais.
    private TokenDTO GenerateToken(User user, IEnumerable<Claim>? existingClaims = null)
    {
        var claims = existingClaims?.ToList() ?? new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
        };

        var accessToken = _tokenService.GenerateAccessToken(claims);   
        var refreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime= DateTime.Now.AddDays(_Configuration.DaysToExpiry);
        _userAuthService.Update(user);


        var created = DateTime.Now;
        var expirationDate = created.AddMinutes(_Configuration.Minutes);
        return new TokenDTO
        {
            Authenticated = true,
            Created = created.ToString(DATE_FORMAT),
            Expiration = expirationDate.ToString(DATE_FORMAT),
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}
