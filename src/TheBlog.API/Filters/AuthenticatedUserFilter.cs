using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using TheBlog.Application.Communication;
using TheBlog.Domain.Errors;
using TheBlog.Domain.Repositories;
using TheBlog.Domain.Security.Tokens;

namespace TheBlog.API.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUserRepository _userRepository;

    public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserRepository userRepository)
    {
        _accessTokenValidator = accessTokenValidator;
        _userRepository = userRepository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var token = TokenOnRequest(context);

        if(token is null)
        {
            context.Result = new UnauthorizedObjectResult(new UnauthorizedError(ErrorMessages.NO_TOKEN));
            return;
        }

        try
        {
            var userId = _accessTokenValidator.ValidateAndGetUserId(token);
            var user = await _userRepository.GetById(userId);

            if(user is null)
            {
                context.Result = new UnauthorizedObjectResult(new UnauthorizedError(ErrorMessages.USER_WITHOUT_PERMISSION));
            }

            context.HttpContext.Items["LoggedUser"] = user;
        }
        catch(SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new UnauthorizedError("Token expired"));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new UnauthorizedError(ErrorMessages.USER_WITHOUT_PERMISSION));
        }
    }

    private static string? TokenOnRequest(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers.Authorization.ToString();

        return authorization.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase)
            ? authorization["Bearer ".Length..].Trim()
            : null;
    }
}
