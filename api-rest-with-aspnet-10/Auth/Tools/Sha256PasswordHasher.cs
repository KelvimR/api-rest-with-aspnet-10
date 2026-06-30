using api_rest_with_aspnet_10.Auth.Contract;
using System.Net.WebSockets;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace api_rest_with_aspnet_10.Auth.Tools;

public class Sha256PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        var inputBytes = Encoding.UTF8.GetBytes(password);
        var hashedBytes = SHA256.HashData(inputBytes);
        
        var builder = new StringBuilder();
        foreach(var b in hashedBytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }

    public bool Verify(string password, string hashedPassword)
    {
        return Hash(password) == hashedPassword;
    }
}
