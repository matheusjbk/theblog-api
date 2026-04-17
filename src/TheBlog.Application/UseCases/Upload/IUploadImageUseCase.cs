using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.Upload;

public interface IUploadImageUseCase
{
    Task<ResultValue<ImageUrlResponse>> Execute(UploadImageRequest request, Domain.Entities.User loggedUser);
}
