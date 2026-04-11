using FluentValidation;
using TheBlog.Application.Communication;
using TheBlog.Application.Communication.Requests;

namespace TheBlog.Application.UseCases.Post.Register;

public class RegisterPostValidator : AbstractValidator<PostRequest>
{
    public RegisterPostValidator()
    {
        RuleFor(request => request.Title).NotEmpty().WithMessage(ErrorMessages.EMPTY_TITLE);
        RuleFor(request => request.Title).MaximumLength(150).WithMessage(ErrorMessages.LONG_TITLE);
        RuleFor(request => request.Excerpt).NotEmpty().WithMessage(ErrorMessages.EMPTY_EXCERPT);
        RuleFor(request => request.Excerpt).MaximumLength(200).WithMessage(ErrorMessages.LONG_EXCERPT);
        RuleFor(request => request.Content).NotEmpty().WithMessage(ErrorMessages.EMPTY_CONTENT);
        RuleFor(request => request.CoverImageUrl).NotEmpty().WithMessage(ErrorMessages.EMPTY_COVER_IMAGE_URL);
        RuleFor(request => request.CoverImageUrl).Must(ValidUrl).WithMessage(ErrorMessages.INVALID_URL_FORMAT);
    }

    private bool ValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
