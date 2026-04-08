using Microsoft.EntityFrameworkCore;
using TheBlog.Domain.Entities;

namespace TheBlog.Infra.DataAccess;

public class TheBlogDbContext : DbContext
{
    public TheBlogDbContext(DbContextOptions<TheBlogDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheBlogDbContext).Assembly);
}
