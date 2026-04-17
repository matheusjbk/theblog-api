using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using TheBlog.Application.Communication;
using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Errors;
using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.Upload;

public class UploadImageUseCase : IUploadImageUseCase
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;

    public UploadImageUseCase(IWebHostEnvironment env, IConfiguration configuration)
    {
        _env = env;
        _configuration = configuration;
    }

    public async Task<ResultValue<ImageUrlResponse>> Execute(UploadImageRequest request, Domain.Entities.User loggedUser)
    {
        using var sourceStream = request.Image.OpenReadStream();

        (var isValidImage, var extension) = ValidateAndGetFileExtension(sourceStream);

        if (!isValidImage) return ResultValue<ImageUrlResponse>.Failure(new ValidationError([ErrorMessages.ONLY_IMAGES_ACCEPTED]));

        var rootPath = _env.WebRootPath ?? Path.Combine(AppContext.BaseDirectory, "wwwroot");
        var uploadDir = _configuration.GetValue<string>("Settings:Images:UploadDir")!;
        var today = DateTime.UtcNow.Date.ToShortDateString().Replace("/", "-");
        var uploadsPath = Path.Combine(rootPath, uploadDir, today);

        if(!Directory.Exists(uploadsPath)) Directory.CreateDirectory(uploadsPath);

        var fileName = $"{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(uploadsPath, fileName);

        var options = new FileStreamOptions
        {
            Mode = FileMode.Create,
            Access = FileAccess.Write,
            Options = FileOptions.Asynchronous | FileOptions.SequentialScan
        };

        using var destinationStream = new FileStream(fullPath, options);

        await sourceStream.CopyToAsync(destinationStream);

        var imageUrlResponse = new ImageUrlResponse
        {
            Url = $"/{uploadDir}/{today}/{fileName}"
        };

        return ResultValue<ImageUrlResponse>.Success(imageUrlResponse);
    }

    private static (bool isValidImage, string extension) ValidateAndGetFileExtension(Stream stream)
    {
        (bool isValid, string ext) result = stream switch
        {
            _ when stream.Is<PortableNetworkGraphic>() => (true, NormalizeExtension(PortableNetworkGraphic.TypeExtension)),
            _ when stream.Is<JointPhotographicExpertsGroup>() => (true, NormalizeExtension(JointPhotographicExpertsGroup.TypeExtension)),
            _ when stream.Is<Webp>() => (true, NormalizeExtension(Webp.TypeExtension)),
            _ when stream.Is<GraphicsInterchangeFormat89>() => (true, NormalizeExtension(GraphicsInterchangeFormat89.TypeExtension)),
            _ => (false, string.Empty)
        };

        stream.Position = 0;

        return result;
    }

    private static string NormalizeExtension(string extension) => extension.StartsWith('.') ? extension : $".{extension}";
}
