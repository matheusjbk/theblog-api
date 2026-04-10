using FluentValidation;
using FluentValidation.Validators;
using TheBlog.Application.Communication;

namespace TheBlog.Application.SharedValidators;

internal class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if(string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", ErrorMessages.EMPTY_PASSWORD);
            return false;
        }

        if(password.Length <= 5)
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", ErrorMessages.INVALID_PASSWORD);
            return false;
        }

        return true;
    }

    protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
}
