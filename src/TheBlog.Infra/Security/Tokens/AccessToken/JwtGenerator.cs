using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TheBlog.Domain.Entities;
using TheBlog.Domain.Security.Tokens;

namespace TheBlog.Infra.Security.Tokens.AccessToken;

public class JwtGenerator : JwtHandler, IAccessTokenGenerator
{
    private readonly uint _expirationTimeInMinutes;
    private readonly string _signingKey;

    public JwtGenerator(uint expirationTimeInMinutes, string signingKey)
    {
        _expirationTimeInMinutes = expirationTimeInMinutes;
        _signingKey = signingKey;
    }

    public string GenerateToken(User user)
    {
        var credentials = new SigningCredentials(
            GenerateSecurityKey(_signingKey),
            SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeInMinutes),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
