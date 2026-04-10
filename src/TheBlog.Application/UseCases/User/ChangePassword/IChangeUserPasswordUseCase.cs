using TheBlog.Application.Communication.Requests;
using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.User.ChangePassword;

public interface IChangeUserPasswordUseCase
{
    public Task<Result> Execute(ChangePasswordRequest request, Domain.Entities.User loggedUser);
}
