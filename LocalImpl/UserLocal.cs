using System.Text.Json;
using LocalImpl.Util;
using RepoContracts;
using ServerEntities;

namespace LocalImpl;

/// <summary>
/// The UserLocal class manages User entities in a local context.
/// </summary>
public sealed class UserLocal : EntityLocal<User>, IUserRepo {
    /// <summary>
    /// A constant string that represents the type of the entity, specifically "User".
    /// Used internally to identify and work with this specific entity type within the UserLocal class.
    /// </summary>
    private const string EntityType = "User";

    /// <summary>
    /// A private static field that holds the singleton instance of the UserLocal class.
    /// This ensures that only one instance of UserLocal exists throughout the application's lifecycle.
    /// </summary>
    private static UserLocal? _instance;

    /// <summary>
    /// A private field that holds a list of User entities. This list is
    /// initialized by deserializing data from a JSON file located at
    /// the path specified in FilePaths.UsersPath. If the deserialization
    /// process fails or results in null, an empty list is assigned to this field.
    /// </summary>
    private List<User> _list = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(FilePaths.UsersPath)) ?? [];

    /// <summary>
    /// Retrieves the singleton instance of UserLocal.
    /// </summary>
    /// <returns>The singleton instance of UserLocal.</returns>
    public static UserLocal Get() {
        if (_instance is null) return _instance = new UserLocal();
        return _instance;
    }

    /// <summary>
    /// Provides local implementation for User repository following singleton pattern.
    /// </summary>
    private UserLocal() {
        Build(EntityType);
    }

    /// <summary>
    /// Retrieves all User entities as an IQueryable collection.
    /// </summary>
    /// <returns>An IQueryable collection of User entities.</returns>
    public IQueryable<User> GetAll() {
        return _GetAll(_list);
    }

    /// <summary>
    /// Asynchronously retrieves a User entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the User entity to retrieve.</param>
    /// <returns>A task representing the asynchronous get operation, with the User entity as the result.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when no User entity with the specified id exists.</exception>
    public async Task<User> GetAsync(int id) {
        return await _GetAsync(_list, id);
    }

    /// <summary>
    /// Asynchronously retrieves a User entity by its username.
    /// </summary>
    /// <param name="username">The username of the User entity to retrieve.</param>
    /// <returns>A task representing the asynchronous get operation, with the User entity as the result.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no User entity with the specified username exists.</exception>
    public async Task<User> GetByUsernameAsync(string username) {
        var user = _list.FirstOrDefault(u => u.Username == username);
        if (user == null) throw new InvalidOperationException($"{EntityType} named '{username}' does not exist!");
        return await Task.FromResult(user);
    }

    /// <summary>
    /// Asynchronously retrieves a User entity by its email address.
    /// </summary>
    /// <param name="email">The email address of the User entity to retrieve.</param>
    /// <returns>A task representing the asynchronous get operation, with the User entity as the result.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no User entity with the specified email exists.</exception>
    public async Task<User> GetByEmailAsync(string email) {
        var user = _list.FirstOrDefault(u => u.Email == email);
        if (user == null) throw new InvalidOperationException($"{EntityType} with email '{email}' does not exist!");
        return await Task.FromResult(user);
    }

    /// <summary>
    /// Asynchronously adds a new User entity to the local list and saves the updated list.
    /// </summary>
    /// <param name="user">The User entity to add.</param>
    /// <returns>A task representing the asynchronous add operation, with the added User entity as the result.</returns>
    public async Task<User> AddAsync(User user) {
        _list = await _AddAsync(_list, user);
        await Save();
        return user;
    }

    /// <summary>
    /// Asynchronously updates the User entity in the local list and saves the updated list.
    /// </summary>
    /// <param name="user">The User entity to update.</param>
    /// <returns>A task representing the asynchronous update operation.</returns>
    public async Task UpdateAsync(User user) {
        _list = await _UpdateAsync(_list, user);
        await Save();
    }

    /// <summary>
    /// Asynchronously deletes the User entity with the specified ID from the local list and saves the updated list.
    /// </summary>
    /// <param name="id">The ID of the User entity to delete.</param>
    /// <returns>A task representing the asynchronous delete operation.</returns>
    /// <exception cref="IOException">Thrown when the delete operation could not complete due to an I/O error.</exception>
    public async Task DeleteAsync(int id) {
        _list = await _DeleteAsync(_list, id);
        await Save();
    }

    /// Asynchronously saves the current list of User entities to the predefined file path.
    /// <exception cref="IOException">Thrown when the data could not be saved to the file.</exception>
    private async Task Save() {
        await _Save(_list, FilePaths.UsersPath);
    }
}