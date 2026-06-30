namespace api_rest_with_aspnet_10.Auth.Contract;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password,string hashedPassword);
}
