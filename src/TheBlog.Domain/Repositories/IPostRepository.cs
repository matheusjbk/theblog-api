using TheBlog.Domain.Entities;

namespace TheBlog.Domain.Repositories;

public interface IPostRepository
{
    Task Add(Post post);
    Task<Post?> GetByIdOwned(Guid id, User user);
    Task<Post?> GetById(Guid id);
    Task<IEnumerable<Post>> GetAllOwned(User user);
    Task<IEnumerable<Post>> GetAll();
}
