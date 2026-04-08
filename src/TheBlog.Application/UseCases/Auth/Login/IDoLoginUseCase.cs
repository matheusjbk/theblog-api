using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.Auth.Login;

public interface IDoLoginUseCase
{
    public Task<ResultValue<LoginResponse>> Execute(LoginRequest request);
}
