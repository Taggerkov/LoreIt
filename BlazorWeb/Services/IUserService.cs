using APILibrary;

namespace BlazorWeb.Services;

/// <summary>
/// Provides methods for user-related operations such as retrieving, adding, updating, and deleting user information.
/// </summary>
public interface IUserService {
    /// <summary>
    /// Retrieves all users from the data store.
    /// </summary>
    /// <returns>An IQueryable collection of UserDto objects containing the details of all users.</returns>
    public IQueryable<UserDto?> GetAll();

    /// <summary>
    /// Retrieves a user asynchronously based on the provided user ID.
    /// </summary>
    /// <param name="id">The ID of the user to be retrieved.</param>
    /// <returns>A Task representing the asynchronous operation, with a UserDto containing the details of the retrieved user.</returns>
    public Task<UserDto?> GetAsync(int id);

    /// <summary>
    /// Retrieves a user asynchronously based on the provided username.
    /// </summary>
    /// <param name="username">The username of the user to be retrieved.</param>
    /// <returns>A Task representing the asynchronous operation, with a UserDto containing the details of the retrieved user.</returns>
    public Task<UserDto?> GetByUsernameAsync(string username);

    /// <summary>
    /// Retrieves a user asynchronously based on the provided email.
    /// </summary>
    /// <param name="email">The email of the user to be retrieved.</param>
    /// <returns>A Task representing the asynchronous operation, with a UserDto containing the details of the retrieved user.</returns>
    public Task<UserDto?> GetByEmailAsync(string email);

    /// <summary>
    /// Adds a new user asynchronously based on the provided user data.
    /// </summary>
    /// <param name="request">The data of the user to be added.</param>
    /// <returns>A Task representing the asynchronous operation, with a UserDto containing the details of the added user.</returns>
    public Task<UserDto> AddAsync(UserDto request);

    /// <summary>
    /// Updates a user asynchronously with the given user ID and new data.
    /// </summary>
    /// <param name="id">The ID of the user to be updated.</param>
    /// <param name="request">The new data for updating the user.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public Task UpdateAsync(int id, UserDto request);

    /// <summary>
    /// Deletes a user asynchronously by the given user ID.
    /// </summary>
    /// <param name="id">The ID of the user to be deleted.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public Task DeleteAsync(int id);
}