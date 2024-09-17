using ServerEntities;

namespace RepoContracts;

/// Interface for user repository operations.
public interface IUserRepo {
    /// Retrieves all users from the repository.
    /// <returns>
    /// An IQueryable collection of User entities from the repository.
    /// </returns>
    IQueryable<User> GetAllUsers();

    /// Asynchronously retrieves a user by their identifier.
    /// <param name="id">The unique identifier of the user to be retrieved.</param>
    /// <returns>A Task representing the asynchronous operation, with a User entity as the result.</returns>
    Task<User> GetByIdAsync(int id);

    /// Asynchronously retrieves a user by their username.
    /// <param name="username">The username of the user to be retrieved.</param>
    /// <returns>A Task representing the asynchronous operation, with a User entity as the result.</returns>
    Task<User> GetByUsernameAsync(string username);

    /// Asynchronously retrieves a user by their email address.
    /// <param name="email">The email address of the user to be retrieved.</param>
    /// <returns>A Task representing the asynchronous operation, with a User entity as the result.</returns>
    Task<User> GetByEmailAsync(string email);

    /// Asynchronously adds a new user to the repository.
    /// <param name="user">The user entity to be added to the repository.</param>
    /// <returns>
    /// A Task representing the asynchronous operation, with a User entity as the result.
    /// </returns>
    Task<User> AddAsync(User user);

    /// Updates an existing user in the repository.
    /// <param name="user">
    /// The User entity with updated information to be saved to the repository.
    /// </param>
    /// <return>
    /// A task representing the asynchronous operation.
    /// </return>
    Task UpdateAsync(User user);

    /// Deletes a user from the repository asynchronously.
    /// <param name="id">The unique identifier of the user to be deleted.</param>
    /// <return>A Task representing the asynchronous operation.</return>
    Task DeleteAsync(int id);
}