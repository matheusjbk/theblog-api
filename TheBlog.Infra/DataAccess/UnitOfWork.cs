using TheBlog.Domain.Repositories;

namespace TheBlog.Infra.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly TheBlogDbContext _context;

    public UnitOfWork(TheBlogDbContext context) => _context = context;

    public async Task CommitAsync() => await _context.SaveChangesAsync();
}
