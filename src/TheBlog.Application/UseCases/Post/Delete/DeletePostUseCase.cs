using TheBlog.Application.Communication;
using TheBlog.Domain.Errors;
using TheBlog.Domain.Primitives;
using TheBlog.Domain.Repositories;

namespace TheBlog.Application.UseCases.Post.Delete;

public class DeletePostUseCase : IDeletePostUseCase
{
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePostUseCase(IPostRepository postRepository, IUnitOfWork unitOfWork)
    {
        _postRepository = postRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Execute(Guid id, Domain.Entities.User loggedUser)
    {
        var post = await _postRepository.GetByIdToUpdate(id, loggedUser);

        if (post is null) return Result.Failure(new NotFoundError(ErrorMessages.POST_NOT_FOUND));

        _postRepository.Delete(post);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }
}
