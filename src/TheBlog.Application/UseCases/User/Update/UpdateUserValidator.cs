using FluentValidation;
using TheBlog.Application.Communication;
using TheBlog.Application.Communication.Requests;

namespace TheBlog.Application.UseCases.User.Update;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        When(request => !string.IsNullOrEmpty(request.Email), () => RuleFor(request => request.Email).EmailAddress().WithMessage(ErrorMessages.INVALID_EMAIL));
    }
}
