using Microsoft.EntityFrameworkCore;
using TheBlog.Domain.Entities;
using TheBlog.Domain.Repositories;

namespace TheBlog.Infra.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TheBlogDbContext _context;

    public UserRepository(TheBlogDbContext context) => _context = context;

    public async Task Add(User user) => await _context.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) => await _context.Users.AnyAsync(user => user.Active && user.Email.Equals(email));

    public async Task<User?> GetByEmail(string email) => await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email));

    public async Task<User?> GetById(Guid id) => await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Active && user.Id.Equals(id));

    public async Task<User> GetToUpdateById(Guid id) => await _context.Users.FirstAsync(user => user.Active && user.Id.Equals(id));

    public void Update(User user) => _context.Users.Update(user);
}
