using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Application.MappingConfigurations;
using TheBlog.Domain.Errors;
using TheBlog.Domain.Primitives;
using TheBlog.Domain.Repositories;

namespace TheBlog.Application.UseCases.User.Update;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultValue<RegisteredUserResponse>> Execute(UpdateUserRequest request, Domain.Entities.User loggedUser)
    {
        var validationResult = await Validate(request, loggedUser.Email);

        if (!validationResult.IsSuccess) return ResultValue<RegisteredUserResponse>.Failure(validationResult.Error!);

        var user = await _userRepository.GetToUpdateById(loggedUser.Id);

        user.Name = request.Name ?? user.Name;
        user.Email = request.Email ?? user.Email;

        _userRepository.Update(user);
        await _unitOfWork.CommitAsync();

        return ResultValue<RegisteredUserResponse>.Success(user.ToRegisteredUserResponse());
    }

    private async Task<Result> Validate(UpdateUserRequest request, string currentEmail)
    {
        var validationResult = new UpdateUserValidator().Validate(request);

        if(request.Email is not null)
        {
            if (!currentEmail.Equals(request.Email))
            {
                var emailExistsInDb = await _userRepository.ExistActiveUserWithEmail(request.Email);

                if (emailExistsInDb) return Result.Failure(new ConflictError("Email already registered in database"));
            }
        }

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return Result.Failure(new ValidationError(errors));
        }

        return Result.Success();
    }
}
