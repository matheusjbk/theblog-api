using FluentValidation;
using TheBlog.Application.Communication.Requests;
using TheBlog.Application.SharedValidators;

namespace TheBlog.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(request => request.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(request => request.Password).SetValidator(new PasswordValidator<RegisterUserRequest>());

        When(request => !string.IsNullOrEmpty(request.Email), () => RuleFor(request => request.Email).EmailAddress().WithMessage("Email must be a valid e-mail address"));
    }
}
