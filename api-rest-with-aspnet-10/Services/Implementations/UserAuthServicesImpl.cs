using api_rest_with_aspnet_10.Auth.Contract;
using api_rest_with_aspnet_10.Data.DTO.V1;
using api_rest_with_aspnet_10.Models;
using api_rest_with_aspnet_10.Repositories;

namespace api_rest_with_aspnet_10.Services.Implementations;

public class UserAuthServicesImpl(IUserRepository userRepository, IPasswordHasher passwordHasher) : IUserAuthService
{

    //Constructor primary syntax automatically creates private readonly fields for the parameters passed to the constructor.
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public User? FindByUserName(string username)
    {
        return _userRepository.FindByUserName(username);
    }

    public User Create(AccountCredentialsDTO dto)
    {
        if(dto == null) throw new ArgumentNullException(nameof(dto));

        var user = new User
        {
            Username = dto.Username,
            Fullname = dto.Fullname,
            Password = _passwordHasher.Hash(dto.Password),
            RefreshToken = string.Empty,
            RefreshTokenExpiryTime = null
        };

        return _userRepository.Create(user);
    }

    public bool RevokeToken(string username)
    {
        var user = _userRepository.FindByUserName(username);
        if (user == null) return false;
        
        user.RefreshToken = null;        
        _userRepository.Update(user);
        
        return true;
    }

    public User Update(User user)
    {
        return _userRepository.Update(user);
    }
}
