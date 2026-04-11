using TheBlog.Domain.Entities;
using TheBlog.Domain.Repositories;

namespace TheBlog.Infra.DataAccess.Repositories;

public class PostRepository : IPostRepository
{
    private readonly TheBlogDbContext _context;

    public PostRepository(TheBlogDbContext context) => _context = context;

    public async Task Add(Post post) => await _context.Posts.AddAsync(post);
}
