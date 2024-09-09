using ServerEntities;

namespace RepoContracts;

public interface IUserRepo {
    IQueryable<User> GetAllUsers();
    Task<User> GetByIdAsync(int id);
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
}