using RepoContracts.Util;
using ServerEntities;

namespace RepoContracts;

/// Interface for user repository operations.
public interface IUserRepo : IServerEntityRepo<User> {
    /// Asynchronously retrieves a user by their username.
    /// <param name="username">The username of the user to be retrieved.</param>
    /// <returns>A Task representing the asynchronous operation, with a User entity as the result.</returns>
    Task<User?> GetByUsernameAsync(string username);

    /// Asynchronously retrieves a user by their email address.
    /// <param name="email">The email address of the user to be retrieved.</param>
    /// <returns>A Task representing the asynchronous operation, with a User entity as the result.</returns>
    Task<User> GetByEmailAsync(string email);
}