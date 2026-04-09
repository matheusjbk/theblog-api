namespace TheBlog.Domain.Repositories;

public interface IUserRepository
{
    Task Add(Entities.User user);
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<Entities.User?> GetByEmail(string email);
    Task<Entities.User?> GetById(Guid id);
}
