using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.User.Delete;

public interface IDeleteUserUseCase
{
    public Task<Result> Execute(Domain.Entities.User loggedUser);
}
