using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Entities;

namespace TheBlog.Application.MappingConfigurations;

public static class MapsExtensions
{
    // Request to Entity
    public static User ToUserEntity(this RegisterUserRequest request)
    {
        return new User
        {
            Name = request.Name,
            Email = request.Email
        };
    }

    // Entity to Response
    public static RegisteredUserResponse ToRegisteredUserResponse(this User user)
    {
        return new RegisteredUserResponse
        {
            Name = user.Name,
            Email = user.Email
        };
    }
}
