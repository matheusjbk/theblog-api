using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.Post.GetAll;

public interface IGetAllPostsUseCase
{
    Task<ResultValue<IEnumerable<PostResponse>?>> Execute();
}
