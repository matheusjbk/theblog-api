using TheBlog.Domain.Entities;

namespace TheBlog.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    public string GenerateToken(User user);
}
