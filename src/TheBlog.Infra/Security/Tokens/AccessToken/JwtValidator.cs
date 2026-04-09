using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TheBlog.Domain.Security.Tokens;

namespace TheBlog.Infra.Security.Tokens.AccessToken;

public class JwtValidator : JwtHandler, IAccessTokenValidator
{
    private readonly string _signingKey;

    public JwtValidator(string signingKey) => _signingKey = signingKey;

    public Guid ValidateAndGetUserId(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = GenerateSecurityKey(_signingKey),
            ClockSkew = new TimeSpan(0)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);

        var id = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        return Guid.Parse(id);
    }
}
