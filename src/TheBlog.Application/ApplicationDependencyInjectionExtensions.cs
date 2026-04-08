using Microsoft.Extensions.DependencyInjection;
using TheBlog.Application.UseCases.Auth.Login;
using TheBlog.Application.UseCases.User.Register;

namespace TheBlog.Application;

public static class ApplicationDependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
    }
}
