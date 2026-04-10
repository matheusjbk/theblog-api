using FluentValidation;
using TheBlog.Application.Communication.Requests;
using TheBlog.Application.SharedValidators;

namespace TheBlog.Application.UseCases.User.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(request => request.CurrentPassword).SetValidator(new PasswordValidator<ChangePasswordRequest>());
        RuleFor(request => request.NewPassword).SetValidator(new PasswordValidator<ChangePasswordRequest>());
    }
}
