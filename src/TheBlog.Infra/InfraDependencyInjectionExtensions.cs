using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheBlog.Domain.Repositories;
using TheBlog.Domain.Security.Cryptography;
using TheBlog.Domain.Security.Tokens;
using TheBlog.Infra.DataAccess;
using TheBlog.Infra.DataAccess.Repositories;
using TheBlog.Infra.Security.Cryptography;
using TheBlog.Infra.Security.Tokens.AccessToken;

namespace TheBlog.Infra;

public static class InfraDependencyInjectionExtensions
{
    public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext_SqlServer(services, configuration);
        AddRepositories(services);
        AddPasswordEncrypter(services);
        AddTokens(services, configuration);
    }

    private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer");

        services.AddDbContext<TheBlogDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddPasswordEncrypter(IServiceCollection services) => services.AddScoped<IPasswordEncrypter, BCryptNet>();

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeInMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeInMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey")!;

        services.AddScoped<IAccessTokenGenerator>(provider => new JwtGenerator(expirationTimeInMinutes, signingKey));
        services.AddScoped<IAccessTokenValidator>(provider => new JwtValidator(signingKey));
    }
}
