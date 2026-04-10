using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheBlog.API.Attributes;
using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Application.UseCases.User.ChangePassword;
using TheBlog.Application.UseCases.User.Delete;
using TheBlog.Application.UseCases.User.Profile;
using TheBlog.Application.UseCases.User.Register;
using TheBlog.Application.UseCases.User.Update;
using TheBlog.Domain.Entities;
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

    [HttpPatch("me")]
    [ProducesResponseType(typeof(RegisteredUserResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IError), StatusCodes.Status409Conflict)]
    [AuthenticatedUser]
    public async Task<IActionResult> Update(IUpdateUserUseCase useCase, UpdateUserRequest request, IHttpContextAccessor contextAccessor)
    {
        var loggedUser = contextAccessor.HttpContext!.Items["LoggedUser"] as User;

        var result = await useCase.Execute(request, loggedUser!);

        if(!result.IsSuccess)
        {
            if(result.Error!.StatusCode == HttpStatusCode.Conflict) return Conflict(result.Error);

            if(result.Error!.StatusCode == HttpStatusCode.BadRequest) return BadRequest(result.Error);
        }

        return NoContent();
    }

    [HttpPatch("me/password")]
    [ProducesResponseType(typeof(RegisteredUserResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IError), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> ChangePassword(IChangeUserPasswordUseCase useCase, ChangePasswordRequest request, IHttpContextAccessor contextAccessor)
    {
        var loggedUser = contextAccessor.HttpContext!.Items["LoggedUser"] as User;

        var result = await useCase.Execute(request, loggedUser!);

        if (!result.IsSuccess) return BadRequest(result.Error);

        return NoContent();
    }

    [HttpGet("me")]
    [ProducesResponseType(typeof(RegisteredUserResponse), StatusCodes.Status200OK)]
    [AuthenticatedUser]
    public async Task<IActionResult> GetUserProfile(IGetUserProfileUseCase useCase, IHttpContextAccessor httpContextAccessor)
    {
        var loggedUser = httpContextAccessor.HttpContext!.Items["LoggedUser"] as User;

        return Ok(await useCase.Execute(loggedUser!));
    }

    [HttpDelete("me")]
    [ProducesResponseType(typeof(RegisteredUserResponse), StatusCodes.Status204NoContent)]
    [AuthenticatedUser]
    public async Task<IActionResult> Delete(IDeleteUserUseCase useCase, IHttpContextAccessor httpContextAccessor)
    {
        var loggedUser = httpContextAccessor.HttpContext!.Items["LoggedUser"] as User;

        _ = await useCase.Execute(loggedUser!);

        return NoContent();
    }
}
