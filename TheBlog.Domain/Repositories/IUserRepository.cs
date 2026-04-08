namespace TheBlog.Domain.Repositories;

public interface IUserRepository
{
    Task Add(Entities.User user);
    Task<bool> ExistActiveUserWithEmail(string email);
}
