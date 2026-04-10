using TheBlog.Application.Communication.Responses;

namespace TheBlog.Application.UseCases.User.Profile;

public interface IGetUserProfileUseCase
{
    public Task<RegisteredUserResponse> Execute(Domain.Entities.User loggedUser);
}
