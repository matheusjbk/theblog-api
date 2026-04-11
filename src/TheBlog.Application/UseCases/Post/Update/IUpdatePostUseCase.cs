using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.Post.Update;

public interface IUpdatePostUseCase
{
    Task<ResultValue<PostResponse>> Execute(Guid id, UpdatePostRequest request, Domain.Entities.User user);
}
