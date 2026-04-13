using TheBlog.Domain.Primitives;

namespace TheBlog.Application.UseCases.Post.Delete;

public interface IDeletePostUseCase
{
    Task<Result> Execute(Guid id, Domain.Entities.User loggedUser);
}
