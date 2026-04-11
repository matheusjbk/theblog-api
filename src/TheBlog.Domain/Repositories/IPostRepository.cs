using TheBlog.Domain.Entities;

namespace TheBlog.Domain.Repositories;

public interface IPostRepository
{
    Task Add(Post post);
    Task<Post?> GetByIdOwned(Guid id, User user);
    Task<Post?> GetByIdToUpdate(Guid id, User user);
    Task<Post?> GetBySlug(string slug);
    Task<IEnumerable<Post>> GetAllOwned(User user);
    Task<IEnumerable<Post>> GetAll();
    void Update(Post post);
}
