using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Application.MappingConfigurations;
using TheBlog.Domain.Errors;
using TheBlog.Domain.Primitives;
using TheBlog.Domain.Repositories;
using TheBlog.Domain.Security.Cryptography;

namespace TheBlog.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(IUserRepository userRepository, IPasswordEncrypter passwordEncrypter, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordEncrypter = passwordEncrypter;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultValue<RegisteredUserResponse>> Execute(RegisterUserRequest request)
    {
        var validationResult = await Validate(request);

        if (!validationResult.IsSuccess) return ResultValue<RegisteredUserResponse>.Failure(validationResult.Error!);

        var user = request.ToUserEntity();
        user.Password = _passwordEncrypter.Encrypt(request.Password);

        await _userRepository.Add(user);
        await _unitOfWork.CommitAsync();

        return ResultValue<RegisteredUserResponse>.Success(user.ToRegisteredUserResponse());

    }

    private async Task<Result> Validate(RegisterUserRequest request)
    {
        var validationResult = new RegisterUserValidator().Validate(request);

        var emailExistsInDb = await _userRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExistsInDb) return Result.Failure(new ConflictError("Email already registered in database"));

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return Result.Failure(new ValidationError(errors));
        }

        return Result.Success();
    }
}
