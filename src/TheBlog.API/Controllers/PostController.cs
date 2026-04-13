using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheBlog.API.Attributes;
using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Application.UseCases.Post.Delete;
using TheBlog.Application.UseCases.Post.GetAll;
using TheBlog.Application.UseCases.Post.GetAllOwned;
using TheBlog.Application.UseCases.Post.GetByIdOwned;
using TheBlog.Application.UseCases.Post.GetBySlug;
using TheBlog.Application.UseCases.Post.Register;
using TheBlog.Application.UseCases.Post.Update;
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

    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBySlug(IGetPostBySlugUseCase useCase, string slug)
    {
        var result = await useCase.Execute(slug);

        if(!result.IsSuccess) return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(IGetAllPostsUseCase useCase)
    {
        var result = await useCase.Execute();

        return Ok(result.Value);
    }

    [HttpGet("me/{id}")]
    [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IError), StatusCodes.Status404NotFound)]
    [AuthenticatedUser]
    public async Task<IActionResult> GetByIdOwned(IGetPostByIdOwnedUseCase useCase, string id, IHttpContextAccessor contextAccessor)
    {
        var loggedUser = contextAccessor.HttpContext!.Items["LoggedUser"] as User;

        var result = await useCase.Execute(Guid.Parse(id), loggedUser!);

        if(!result.IsSuccess) return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("me")]
    [ProducesResponseType(typeof(PostResponse), StatusCodes.Status200OK)]
    [AuthenticatedUser]
    public async Task<IActionResult> GetAllOwned(IGetAllPostsOwnedUseCase useCase, IHttpContextAccessor contextAccessor)
    {
        var loggedUser = contextAccessor.HttpContext!.Items["LoggedUser"] as User;

        var result = await useCase.Execute(loggedUser!);

        return Ok(result.Value);
    }

    [HttpPatch("me/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IError), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IError), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> Update(IUpdatePostUseCase useCase, string id, UpdatePostRequest request, IHttpContextAccessor contextAccessor)
    {
        var loggedUser = contextAccessor.HttpContext!.Items["LoggedUser"] as User;

        var result = await useCase.Execute(Guid.Parse(id), request, loggedUser!);

        if(!result.IsSuccess)
        {
            if(result.Error!.StatusCode == HttpStatusCode.NotFound) return NotFound(result.Error);

            if(result.Error!.StatusCode == HttpStatusCode.BadRequest) return BadRequest(result.Error);
        }

        return NoContent();
    }

    [HttpDelete("me/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IError), StatusCodes.Status404NotFound)]
    [AuthenticatedUser]
    public async Task<IActionResult> Delete(IDeletePostUseCase useCase, string id, IHttpContextAccessor contextAccessor)
    {
        var loggedUser = contextAccessor.HttpContext!.Items["LoggedUser"] as User;

        var result = await useCase.Execute(Guid.Parse(id), loggedUser!);

        if(!result.IsSuccess) return NotFound(result.Error);

        return NoContent();
    }
}
