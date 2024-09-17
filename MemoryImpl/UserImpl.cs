using MemoryRepo.Util;
using ServerEntities;
using RepoContracts;
using ServerEntities.Util;

namespace MemoryRepo;

/// Provides an implementation of the IUserRepo interface with CRUD operations for managing User entities in memory.
public class UserImpl : ICrud, IUserRepo {
    /// Represents a collection of User entities managed by the repository.
    private List<User> Users { get; set; } = [];

    /// Represents the type of the entity as a string.
    private const string EntityType = "User";

    /// Retrieves all users from the repository.
    /// <returns>
    /// An IQueryable collection of User entities from the repository.
    /// </returns>
    public IQueryable<User> GetAllUsers() {
        return Users.AsQueryable();
    }

    /// Retrieves a user by their unique identifier asynchronously.
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the user with the specified identifier.
    /// </returns>
    public Task<User> GetByIdAsync(int id) {
        if (ICrud.Read(GetGenericList(), id, EntityType) is User user) return Task.FromResult(user);
        throw new Exception("Couldn't read User or possible data corruption.");
    }

    /// Retrieves a user by their username asynchronously.
    /// <param name="username">The username of the user to retrieve.</param>
    /// <returns>
    /// A Task that represents the asynchronous operation. The Task result contains the User entity.
    /// Throws InvalidOperationException if the user does not exist.
    /// </returns>
    public Task<User> GetByUsernameAsync(string username) {
        var user = Users.FirstOrDefault(u => u.Username == username);
        if (user == null) throw new InvalidOperationException($"User named '{username}' does not exist!");
        return Task.FromResult(user);
    }

    /// Retrieves a user by their email address asynchronously.
    /// <param name="email">The email address of the user to be retrieved.</param>
    /// <returns>A Task representing the asynchronous operation, with a User entity as the result.</returns>
    public Task<User> GetByEmailAsync(string email) {
        var user = Users.FirstOrDefault(u => u.Email == email);
        if (user == null) throw new InvalidOperationException($"User with email '{email}' does not exist!");
        return Task.FromResult(user);
    }

    /// Asynchronously adds a new user to the repository.
    /// <param name="user">The user entity to be added to the repository.</param>
    /// <returns>
    /// A Task representing the asynchronous operation, with a User entity as the result.
    /// </returns>
    public Task<User> AddAsync(User user) {
        Users = ICrud.Create(GetGenericList(), user).Cast<User>().ToList();
        return Task.FromResult(user);
    }

    /// Updates an existing user in the repository.
    /// <param name="user">
    /// The User entity with updated information to be saved to the repository.
    /// </param>
    /// <return>
    /// A task representing the asynchronous operation.
    /// </return>
    public Task UpdateAsync(User user) {
        Users = ICrud.Update(GetGenericList(), user).Cast<User>().ToList();
        return Task.CompletedTask;
    }

    /// Deletes a user from the repository asynchronously.
    /// <param name="id">The unique identifier of the user to be deleted.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public Task DeleteAsync(int id) {
        Users = ICrud.Delete(GetGenericList(), id, EntityType).Cast<User>().ToList();
        return Task.CompletedTask;
    }

    /// Retrieves the list of users as a generic collection of server entities.
    /// <returns>
    /// An IEnumerable collection of IServerEntity representing the users.
    /// </returns>
    #pragma warning disable CA1859
    private IEnumerable<IServerEntity> GetGenericList() {
        return Users;
    }
}