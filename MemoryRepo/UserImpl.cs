using MemoryRepo.Util;
using ServerEntities;
using RepoContracts;
using ServerEntities.Util;

namespace MemoryRepo;

public class UserImpl : ICrud, IUserRepo {
    private List<User> Users { get; set; } = new();
    private const string EntityType = "User";

    public IQueryable<User> GetAllUsers() {
        return Users.AsQueryable();
    }

    public Task<User> GetByIdAsync(int id) {
        if (ICrud.Read(GetGenericList(), id, EntityType) is User user) return Task.FromResult(user);
        throw new Exception("Couldn't read User or possible data corruption.");
    }

    public Task<User> GetByUsernameAsync(string username) {
        var user = Users.FirstOrDefault(u => u.Username == username);
        if (user == null) throw new InvalidOperationException($"User named '{username}' does not exist!");
        return Task.FromResult(user);
    }

    public Task<User> GetByEmailAsync(string email) {
        var user = Users.FirstOrDefault(u => u.Email == email);
        if (user == null) throw new InvalidOperationException($"User with email '{email}' does not exist!");
        return Task.FromResult(user);
    }
    
    public Task<User> AddAsync(User user) {
        ICrud.Create(GetGenericList(), user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user) {
        ICrud.Update(GetGenericList(), user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id) {
        ICrud.Delete(GetGenericList(), id, EntityType);
        return Task.CompletedTask;
    }
    
    private List<IServerEntity> GetGenericList() {
        var genericList = Users.Cast<IServerEntity>().ToList();
        Users = genericList.Cast<User>().ToList();
        return genericList;
    }
}