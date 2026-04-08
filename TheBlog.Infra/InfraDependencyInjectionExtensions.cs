using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheBlog.Domain.Repositories;
using TheBlog.Domain.Security.Cryptography;
using TheBlog.Infra.DataAccess;
using TheBlog.Infra.DataAccess.Repositories;
using TheBlog.Infra.Security.Cryptography;

namespace TheBlog.Infra;

public static class InfraDependencyInjectionExtensions
{
    public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext_SqlServer(services, configuration);
        AddRepositories(services);
        AddPasswordEncrypter(services);
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
}
