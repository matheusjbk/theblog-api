using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.Post.Register;

public interface IRegisterPostUseCase
{
    public Task<ResultValue<PostResponse>> Execute(PostRequest request, Domain.Entities.User loggedUser);
}
