using TheBlog.Domain.Primitives;
using TheBlog.Domain.Repositories;

namespace TheBlog.Application.UseCases.User.Delete;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Execute(Domain.Entities.User loggedUser)
    {
        var user = await _userRepository.GetToUpdateById(loggedUser.Id);

        _userRepository.Delete(user);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }
}
