using Microsoft.AspNetCore.Mvc;
using TheBlog.API.Attributes;
using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Application.UseCases.Upload;
using TheBlog.Domain.Entities;
using TheBlog.Domain.Errors;

namespace TheBlog.API.Controllers;
public class UploadController : TheBlogBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ImageUrlResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IError), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> Upload(IUploadImageUseCase useCase, UploadImageRequest request, IHttpContextAccessor contextAccessor)
    {
        var loggedUser = contextAccessor.HttpContext!.Items["LoggedUser"] as User;

        var result = await useCase.Execute(request, loggedUser!);

        if (!result.IsSuccess) return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
