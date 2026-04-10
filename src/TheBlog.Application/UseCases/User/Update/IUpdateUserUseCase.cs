using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.User.Update;

public interface IUpdateUserUseCase
{
    public Task<ResultValue<RegisteredUserResponse>> Execute(UpdateUserRequest request, Domain.Entities.User user);
}
