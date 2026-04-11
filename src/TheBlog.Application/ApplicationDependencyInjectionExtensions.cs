using Microsoft.Extensions.DependencyInjection;
using TheBlog.Application.UseCases.Auth.Login;
using TheBlog.Application.UseCases.Post.Register;
using TheBlog.Application.UseCases.User.ChangePassword;
using TheBlog.Application.UseCases.User.Delete;
using TheBlog.Application.UseCases.User.Profile;
using TheBlog.Application.UseCases.User.Register;
using TheBlog.Application.UseCases.User.Update;

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
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangeUserPasswordUseCase, ChangeUserPasswordUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();

        services.AddScoped<IRegisterPostUseCase, RegisterPostUseCase>();

        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
    }
}
