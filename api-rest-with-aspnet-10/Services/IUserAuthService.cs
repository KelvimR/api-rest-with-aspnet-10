using api_rest_with_aspnet_10.Data.DTO.V1;
using api_rest_with_aspnet_10.Models;

namespace api_rest_with_aspnet_10.Services;

public interface IUserAuthService
{
    User? FindByUserName(string username);
    User Create(AccountCredentialsDTO dto);
    User Update(User user);
    bool RevokeToken(string username);
}
