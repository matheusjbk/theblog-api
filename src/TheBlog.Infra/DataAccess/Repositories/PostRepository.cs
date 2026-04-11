using Microsoft.EntityFrameworkCore;
using TheBlog.Domain.Entities;
using TheBlog.Domain.Repositories;

namespace TheBlog.Infra.DataAccess.Repositories;

public class PostRepository : IPostRepository
{
    private readonly TheBlogDbContext _context;

    public PostRepository(TheBlogDbContext context) => _context = context;

    public async Task Add(Post post) => await _context.Posts.AddAsync(post);

    public async Task<Post?> GetByIdOwned(Guid id, User user) =>
        await _context.Posts.Include(post => post.Author)
        .AsNoTracking().FirstOrDefaultAsync(post => post.Id == id && post.AuthorId == user.Id);

    public async Task<Post?> GetById(Guid id) =>
        await _context.Posts.AsNoTracking().FirstOrDefaultAsync(post => post.Id == id);

    public async Task<IEnumerable<Post>> GetAllOwned(User user) =>
        await _context.Posts.Include(post => post.Author)
        .AsNoTracking().Where(post => post.AuthorId == user.Id).ToListAsync();

    public async Task<IEnumerable<Post>> GetAll() =>
        await _context.Posts.AsNoTracking().ToListAsync();
}
