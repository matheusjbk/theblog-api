using TheBlog.Domain.Entities;

namespace TheBlog.Domain.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<User?> GetByEmail(string email);
    Task<User?> GetById(Guid id);
    Task<User> GetToUpdateById(Guid id);
    void Update(User user);
    void Delete(User user);
}
