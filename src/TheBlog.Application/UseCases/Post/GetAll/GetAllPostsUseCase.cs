using TheBlog.Application.Communication.Responses;
using TheBlog.Application.MappingConfigurations;
using TheBlog.Domain.Primitives;
using TheBlog.Domain.Repositories;

namespace TheBlog.Application.UseCases.Post.GetAll;

public class GetAllPostsUseCase : IGetAllPostsUseCase
{
    private readonly IPostRepository _postRepository;

    public GetAllPostsUseCase(IPostRepository postRepository) => _postRepository = postRepository;

    public async Task<ResultValue<IEnumerable<PostResponse>?>> Execute()
    {
        var posts = await _postRepository.GetAll();

        var postsResponse = posts.ToPostResponseList();

        return ResultValue<IEnumerable<PostResponse>?>.Success(postsResponse);
    }
}
