namespace TheBlog.Domain.Security.Tokens;

public interface IAccessTokenValidator
{
    public Guid ValidateAndGetUserId(string token);
}
