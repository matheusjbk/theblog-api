using Microsoft.AspNetCore.Mvc;
using TheBlog.API.Attributes;
using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Application.UseCases.Post.Register;
using TheBlog.Domain.Entities;
using TheBlog.Domain.Errors;

namespace TheBlog.API.Controllers;
public class PostController : TheBlogBaseController
{
    [HttpPost("me")]
    [ProducesResponseType(typeof(PostResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IError), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> Register(IRegisterPostUseCase useCase, PostRequest request, IHttpContextAccessor contextAccessor)
    {
        var loggedUser = contextAccessor.HttpContext!.Items["LoggedUser"] as User;

        var result = await useCase.Execute(request, loggedUser!);

        if(!result.IsSuccess) return BadRequest(result.Error);

        return Created(string.Empty, result.Value);
    }
}
