using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.Post.GetAllOwned;

public interface IGetAllPostsOwnedUseCase
{
    Task<ResultValue<IEnumerable<PostResponse>?>> Execute(Domain.Entities.User loggedUser);
}
