using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheBlog.API.Attributes;
using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Application.UseCases.User.Register;
using TheBlog.Domain.Errors;

namespace TheBlog.API.Controllers;

public class UserController : TheBlogBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(RegisteredUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IError), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(IRegisterUserUseCase useCase, RegisterUserRequest request)
    {
        var result = await useCase.Execute(request);

        if(!result.IsSuccess)
        {
            if(result.Error!.StatusCode == HttpStatusCode.Conflict) return Conflict(result.Error);

            if(result.Error!.StatusCode == HttpStatusCode.BadRequest) return BadRequest(result.Error);

        }

        return Created(string.Empty, result.Value);
    }

    [AuthenticatedUser]
    [HttpGet]
    public async Task<IActionResult> GetUserProfile(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext!.Items["LoggedUser"];

        if (user is not null) return Ok(user);

        return NotFound();
    }
}
