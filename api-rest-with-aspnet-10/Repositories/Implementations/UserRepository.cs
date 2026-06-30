using api_rest_with_aspnet_10.Context;
using api_rest_with_aspnet_10.Models;

namespace api_rest_with_aspnet_10.Repositories.Implementations;

public class UserRepository(MSSQLContext context) : GenericRepository<User>(context), IUserRepository
{
    
    public User FindByUserName(string username)
    {
        return _context.Users.SingleOrDefault(
            u => u.Username == username);
    }
}
