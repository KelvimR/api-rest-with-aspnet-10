using api_rest_with_aspnet_10.Data.DTO.V1;
using Microsoft.AspNetCore.Components.Web;

namespace api_rest_with_aspnet_10.Services;

public interface ILoginService
{
    TokenDTO? ValidateCredentials(UserDTO user);
    TokenDTO? ValidateCredentials(TokenDTO token);
    bool RevokeToken(string username);
    AccountCredentialsDTO? Create(AccountCredentialsDTO user);
}
