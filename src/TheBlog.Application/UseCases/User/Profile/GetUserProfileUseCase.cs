using TheBlog.Application.Communication.Responses;
using TheBlog.Application.MappingConfigurations;

namespace TheBlog.Application.UseCases.User.Profile;

public class GetUserProfileUseCase : IGetUserProfileUseCase
{
    public async Task<RegisteredUserResponse> Execute(Domain.Entities.User loggedUser)
    {
        return loggedUser.ToRegisteredUserResponse();
    }
}
