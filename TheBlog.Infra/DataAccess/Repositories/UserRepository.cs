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
}
