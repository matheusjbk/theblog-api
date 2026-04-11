using FluentValidation;
using TheBlog.Application.Communication;
using TheBlog.Application.Communication.Requests;

namespace TheBlog.Application.UseCases.Post.Update;

public class UpdatePostValidator : AbstractValidator<UpdatePostRequest>
{
    public UpdatePostValidator()
    {
        RuleFor(request => request.Title).MaximumLength(150).WithMessage(ErrorMessages.LONG_TITLE);
        RuleFor(request => request.Excerpt).MaximumLength(200).WithMessage(ErrorMessages.LONG_EXCERPT);
        When(request => !String.IsNullOrWhiteSpace(request.CoverImageUrl), () => RuleFor(request => request.CoverImageUrl).Must(ValidUrl).WithMessage(ErrorMessages.INVALID_URL_FORMAT));
    }

    private bool ValidUrl(string? url)
    {
        return Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
