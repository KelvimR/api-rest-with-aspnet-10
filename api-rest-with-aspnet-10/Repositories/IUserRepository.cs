using api_rest_with_aspnet_10.Models;

namespace api_rest_with_aspnet_10.Repositories;

public interface IUserRepository : IRepository<User>
{
    User? FindByUserName(string username);
}
