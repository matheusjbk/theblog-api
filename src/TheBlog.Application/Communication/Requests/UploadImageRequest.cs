using Microsoft.AspNetCore.Http;

namespace TheBlog.Application.Communication.Requests;

public class UploadImageRequest
{
    public IFormFile Image { get; set; } = default!;
}
