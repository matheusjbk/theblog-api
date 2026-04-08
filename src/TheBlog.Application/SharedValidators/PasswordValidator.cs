using FluentValidation;
using FluentValidation.Validators;

namespace TheBlog.Application.SharedValidators;

internal class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if(string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", "Password is required");
            return false;
        }

        if(password.Length <= 5)
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", "Password must be at least 6 characters long");
            return false;
        }

        return true;
    }

    protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
}
