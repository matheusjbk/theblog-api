using TheBlog.Application.Communication;
using TheBlog.Application.Communication.Responses;
using TheBlog.Application.MappingConfigurations;
using TheBlog.Domain.Errors;
using TheBlog.Domain.Primitives;
using TheBlog.Domain.Repositories;

namespace TheBlog.Application.UseCases.Post.GetByIdOwned;

public class GetPostByIdOwnedUseCase : IGetPostByIdOwnedUseCase
{
    private readonly IPostRepository _postRepository;

    public GetPostByIdOwnedUseCase(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<ResultValue<PostResponse>> Execute(Guid id, Domain.Entities.User loggedUser)
    {
        var post = await _postRepository.GetByIdOwned(id, loggedUser);

        if(post is null) return ResultValue<PostResponse>.Failure(new NotFoundError(ErrorMessages.POST_NOT_FOUND));

        var postResponse = post.ToPostResponse();

        return ResultValue<PostResponse>.Success(postResponse);
    }
}
