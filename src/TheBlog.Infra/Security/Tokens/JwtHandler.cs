using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TheBlog.Domain.Entities;

namespace TheBlog.Infra.Security.Tokens;

public abstract class JwtHandler
{
    protected static SymmetricSecurityKey GenerateSecurityKey(string signingKey)
    {
        var bytes = Encoding.UTF8.GetBytes(signingKey);

        return new SymmetricSecurityKey(bytes);
    }

    protected static ClaimsIdentity GenerateClaims(User user)
    {
        var claims = new ClaimsIdentity();

        claims.AddClaims(
        [
            new(ClaimTypes.Sid, user.Id.ToString()),
            new(ClaimTypes.Name, user.Email)
        ]);

        return claims;
    }
}
