using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.Post.GetBySlug;

public interface IGetPostBySlugUseCase
{
    Task<ResultValue<PostResponse>> Execute(string slug);
}
