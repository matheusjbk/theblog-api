using TheBlog.Domain.Entities;

namespace TheBlog.Domain.Repositories;

public interface IPostRepository
{
    Task Add(Post post);
}
