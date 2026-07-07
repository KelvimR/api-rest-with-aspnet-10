using api_rest_with_aspnet_10.Data.DTO.V1;
using api_rest_with_aspnet_10.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_rest_with_aspnet_10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly ILogger<AuthController> _logger;
    private readonly IUserAuthService _userAuthService;

    public AuthController(ILoginService loginService, ILogger<AuthController> logger, IUserAuthService userAuthService)
    {   
        _loginService = loginService;
        _logger = logger;
        _userAuthService = userAuthService;
    }

    [HttpPost("signin")]
    [AllowAnonymous]
    public IActionResult SignIn([FromBody] UserDTO user)
    {
        _logger.LogInformation("Attempting to sign in user: {username}", user.Username);
        if(user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
        {
            _logger.LogWarning("Invalid user data for sign in attempt");
            return BadRequest("Invalid user data");
        }

        var token = _loginService.ValidateCredentials(user);
        if (token == null) return Unauthorized();

        _logger.LogInformation("User {username} signed in successfully", user.Username);
        return Ok(token);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public IActionResult Refresh([FromBody] TokenDTO token)
    {
        if(token == null) return BadRequest("Invalid token request!");

        var newToken = _loginService.ValidateCredentials(token);
        if (newToken == null) return Unauthorized();

        return Ok(newToken);
    }

    [HttpPost("revoke")]
    [AllowAnonymous]
    public IActionResult Revoke()
    {
        var username = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(username)) return BadRequest("Invalid user context!");
        
        var result = _loginService.RevokeToken(username);
        if(!result) return BadRequest("Invalid user context!");

        return Ok();
    }

    [HttpPost("create")]
    [AllowAnonymous]
    public IActionResult Create([FromBody] AccountCredentialsDTO user)
    {
        if (user == null) return BadRequest("Invalid client request!");

        var createdUser = _loginService.Create(user);
        if (createdUser == null) return BadRequest("Failed to create user");

        return Ok(createdUser);
    }
}
