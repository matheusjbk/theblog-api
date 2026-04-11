using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Application.MappingConfigurations;
using TheBlog.Domain.Errors;
using TheBlog.Domain.Primitives;
using TheBlog.Domain.Repositories;
using TheBlog.Domain.Services;

namespace TheBlog.Application.UseCases.Post.Register;

public class RegisterPostUseCase : IRegisterPostUseCase
{
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterPostUseCase(IPostRepository postRepository, IUnitOfWork unitOfWork)
    {
        _postRepository = postRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultValue<PostResponse>> Execute(PostRequest request, Domain.Entities.User loggedUser)
    {
        var validationResult = await Validate(request);

        if(!validationResult.IsSuccess) return ResultValue<PostResponse>.Failure(validationResult.Error!);

        var post = request.ToPostEntity();
        post.AuthorId = loggedUser.Id;
        post.Slug = SlugGenerator.Generate(request.Title);

        await _postRepository.Add(post);
        await _unitOfWork.CommitAsync();

        var postResponse = post.ToPostResponse();
        postResponse.Author = loggedUser.ToRegisteredUserResponse();

        return ResultValue<PostResponse>.Success(postResponse);
    }

    private async Task<Result> Validate(PostRequest request)
    {
        var validationResult = new RegisterPostValidator().Validate(request);

        if(!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return Result.Failure(new ValidationError(errors));
        }

        return Result.Success();
    }
}
