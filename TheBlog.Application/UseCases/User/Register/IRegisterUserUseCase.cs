using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    public Task<ResultValue<RegisteredUserResponse>> Execute(RegisterUserRequest request);
}
