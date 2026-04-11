using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.Post.GetByIdOwned;

public interface IGetPostByIdOwnedUseCase
{
    Task<ResultValue<PostResponse>> Execute(Guid id, Domain.Entities.User loggedUser);
}
