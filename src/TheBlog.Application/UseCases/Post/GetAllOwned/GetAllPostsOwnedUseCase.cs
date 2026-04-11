using TheBlog.Application.Communication.Responses;
using TheBlog.Application.MappingConfigurations;
using TheBlog.Domain.Primitives;
using TheBlog.Domain.Repositories;

namespace TheBlog.Application.UseCases.Post.GetAllOwned;

public class GetAllPostsOwnedUseCase : IGetAllPostsOwnedUseCase
{
    private readonly IPostRepository _postRepository;

    public GetAllPostsOwnedUseCase(IPostRepository postRepository) => _postRepository = postRepository;

    public async Task<ResultValue<IEnumerable<PostResponse>?>> Execute(Domain.Entities.User loggedUser)
    {
        var posts = await _postRepository.GetAllOwned(loggedUser);

        var postsResponse = posts.ToPostResponseList();

        return ResultValue<IEnumerable<PostResponse>?>.Success(postsResponse);
    }
}
