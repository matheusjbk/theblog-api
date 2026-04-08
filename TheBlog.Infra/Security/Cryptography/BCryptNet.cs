using TheBlog.Domain.Security.Cryptography;

namespace TheBlog.Infra.Security.Cryptography;

public class BCryptNet : IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    public bool IsValid(string password, string passwordHash) => BCrypt.Net.BCrypt.Verify(password, passwordHash);
}
