using TheBlog.Application.Communication;
using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Application.MappingConfigurations;
using TheBlog.Domain.Errors;
using TheBlog.Domain.Primitives;
using TheBlog.Domain.Repositories;
using TheBlog.Domain.Services;

namespace TheBlog.Application.UseCases.Post.Update;

public class UpdatePostUseCase : IUpdatePostUseCase
{
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePostUseCase(IPostRepository postRepository, IUnitOfWork unitOfWork)
    {
        _postRepository = postRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultValue<PostResponse>> Execute(Guid id, UpdatePostRequest request, Domain.Entities.User user)
    {
        var validationResult = await Validate(request);

        if(!validationResult.IsSuccess) return ResultValue<PostResponse>.Failure(validationResult.Error!);

        var post = await _postRepository.GetByIdToUpdate(id, user);

        if(post is null) return ResultValue<PostResponse>.Failure(new NotFoundError(ErrorMessages.POST_NOT_FOUND));

        post.Title = request.Title ?? post.Title;
        post.Excerpt = request.Excerpt ?? post.Excerpt;
        post.Content = request.Content ?? post.Content;
        post.CoverImageUrl = request.CoverImageUrl ?? post.CoverImageUrl;
        post.Active = request.Active ?? post.Active;
        post.UpdatedAt = DateTime.UtcNow;

        if (request.Title is not null) post.Slug = SlugGenerator.Generate(request.Title);

        _postRepository.Update(post);
        await _unitOfWork.CommitAsync();

        var postResponse = post.ToPostResponse();

        return ResultValue<PostResponse>.Success(postResponse);
    }

    private async Task<Result> Validate(UpdatePostRequest request)
    {
        var validationResult = new UpdatePostValidator().Validate(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return Result.Failure(new ValidationError(errors));
        }

        return Result.Success();
    }
}
