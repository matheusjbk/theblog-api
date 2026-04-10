using TheBlog.Application.Communication;
using TheBlog.Application.Communication.Requests;
using TheBlog.Domain.Errors;
using TheBlog.Domain.Primitives;
using TheBlog.Domain.Repositories;
using TheBlog.Domain.Security.Cryptography;

namespace TheBlog.Application.UseCases.User.ChangePassword;

public class ChangeUserPasswordUseCase : IChangeUserPasswordUseCase
{
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeUserPasswordUseCase(IPasswordEncrypter passwordEncrypter, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _passwordEncrypter = passwordEncrypter;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Execute(ChangePasswordRequest request, Domain.Entities.User loggedUser)
    {
        var validationResult = await Validate(request, loggedUser);

        if (!validationResult.IsSuccess) return Result.Failure(validationResult.Error!);

        var user = await _userRepository.GetToUpdateById(loggedUser.Id);

        user.Password = _passwordEncrypter.Encrypt(request.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        _userRepository.Update(user);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }

    private async Task<Result> Validate(ChangePasswordRequest request, Domain.Entities.User user)
    {
        var validationResult = new ChangePasswordValidator().Validate(request);

        if (!_passwordEncrypter.IsValid(request.CurrentPassword, user.Password))
            return Result.Failure(new ValidationError([ErrorMessages.DIFFERENT_CURRENT_PASSWORD]));

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return Result.Failure(new ValidationError(errors));
        }

        return Result.Success();
    }
}
