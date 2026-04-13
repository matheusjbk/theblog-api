using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.Post.Register;

public interface IRegisterPostUseCase
{
    public Task<ResultValue<RegisteredPostResponse>> Execute(RegisterPostRequest request, Domain.Entities.User loggedUser);
}
