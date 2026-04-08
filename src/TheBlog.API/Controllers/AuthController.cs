using Microsoft.AspNetCore.Mvc;
using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Application.UseCases.Auth.Login;
using TheBlog.Domain.Errors;

namespace TheBlog.API.Controllers;
public class AuthController : TheBlogBaseController
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(IDoLoginUseCase useCase, LoginRequest request)
    {
        var result = await useCase.Execute(request);

        if(!result.IsSuccess) return Unauthorized(result.Error);

        return Ok(result.Value);
    }
}
