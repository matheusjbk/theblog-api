using TheBlog.Application.Communication;
using TheBlog.Application.Communication.Responses;
using TheBlog.Application.MappingConfigurations;
using TheBlog.Domain.Errors;
using TheBlog.Domain.Primitives;
using TheBlog.Domain.Repositories;

namespace TheBlog.Application.UseCases.Post.GetBySlug;

public class GetPostBySlugUseCase : IGetPostBySlugUseCase
{
    private readonly IPostRepository _postRepository;

    public GetPostBySlugUseCase(IPostRepository postRepository) => _postRepository = postRepository;

    public async Task<ResultValue<PostResponse>> Execute(string slug)
    {
        var post = await _postRepository.GetBySlug(slug);

        if (post is null) return ResultValue<PostResponse>.Failure(new NotFoundError(ErrorMessages.POST_NOT_FOUND));

        return ResultValue<PostResponse>.Success(post.ToPostResponse());
    }
}
