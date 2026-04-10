using FluentValidation;
using TheBlog.Application.Communication;
using TheBlog.Application.Communication.Requests;
using TheBlog.Application.SharedValidators;

namespace TheBlog.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage(ErrorMessages.EMPTY_NAME);
        RuleFor(request => request.Email).NotEmpty().WithMessage(ErrorMessages.EMPTY_EMAIL);
        RuleFor(request => request.Password).SetValidator(new PasswordValidator<RegisterUserRequest>());

        When(request => !string.IsNullOrEmpty(request.Email), () => RuleFor(request => request.Email).EmailAddress().WithMessage(ErrorMessages.INVALID_EMAIL));
    }
}
