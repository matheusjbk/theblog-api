using Microsoft.Extensions.DependencyInjection;
using TheBlog.Application.UseCases.Auth.Login;
using TheBlog.Application.UseCases.Post.Delete;
using TheBlog.Application.UseCases.Post.GetAll;
using TheBlog.Application.UseCases.Post.GetAllOwned;
using TheBlog.Application.UseCases.Post.GetByIdOwned;
using TheBlog.Application.UseCases.Post.GetBySlug;
using TheBlog.Application.UseCases.Post.Register;
using TheBlog.Application.UseCases.Post.Update;
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
        services.AddScoped<IGetPostBySlugUseCase, GetPostBySlugUseCase>();
        services.AddScoped<IGetAllPostsUseCase, GetAllPostsUseCase>();
        services.AddScoped<IGetPostByIdOwnedUseCase, GetPostByIdOwnedUseCase>();
        services.AddScoped<IGetAllPostsOwnedUseCase, GetAllPostsOwnedUseCase>();
        services.AddScoped<IUpdatePostUseCase, UpdatePostUseCase>();
        services.AddScoped<IDeletePostUseCase, DeletePostUseCase>();

        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
    }
}
