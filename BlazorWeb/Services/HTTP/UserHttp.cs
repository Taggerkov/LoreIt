using System.Text.Json;
using APILibrary;

namespace BlazorWeb.Services.HTTP;

/// <summary>
/// Provides an implementation of the <see cref="IUserService"/> interface for managing user data
/// via HTTP requests.
/// </summary>
public class UserHttp(HttpClient client) : IUserService {
    /// <summary>
    /// Specifies the base URL for the User API endpoints.
    /// </summary>
    /// <remarks>
    /// This URL is used as a prefix for all HTTP requests targeting the User API, ensuring consistent and centralized configuration of the API endpoint.
    /// </remarks>
    private const string BaseUrl = "http://localhost:5298/api/User";

    /// <summary>
    /// Configures the JSON serialization options to use when interacting with HTTP API endpoints.
    /// </summary>
    /// <remarks>
    /// The JSON options are set to allow case-insensitive property name matching during serialization and deserialization processes,
    /// making it easier to handle JSON data that may have varying case conventions.
    /// </remarks>
    private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    /// <summary>
    /// Retrieves all users from the API.
    /// </summary>
    /// <returns>An <see cref="IQueryable{UserDto}"/> containing all users.</returns>
    /// <exception cref="Exception">Thrown when the retrieval operation fails.</exception>
    public IQueryable<UserDto>? GetAll() {
        var httpResponse = client.GetFromJsonAsync<IQueryable<UserDto>>(BaseUrl);
        if (httpResponse is null) throw new Exception("Failed to retrieve users.");
        return httpResponse.Result;
    }

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>A Task representing the asynchronous operation, with the result being the retrieved UserDto object.</returns>
    /// <exception cref="Exception">Thrown when the retrieval operation fails.</exception>
    public async Task<UserDto> GetAsync(string id) {
        var httpResponse = await client.GetFromJsonAsync<UserDto>($"{BaseUrl}/{id}");
        if (httpResponse == null) throw new Exception("Failed to retrieve user.");
        return httpResponse;
    }

    public Task<UserDto> GetByUsernameAsync(string username) {
        throw new NotImplementedException();
    }

    public Task<UserDto> GetByEmailAsync(string email) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Adds a new user to the system.
    /// </summary>
    /// <param name="request">The UserDto object containing the new user information.</param>
    /// <returns>A Task representing the asynchronous add operation, with the result being the added UserDto object.</returns>
    /// <exception cref="Exception">Thrown when the add operation fails.</exception>
    public async Task<UserDto> AddAsync(UserDto request) {
        var httpResponse = await client.PostAsJsonAsync(BaseUrl, request);
        var response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) throw new Exception(response);
        return JsonSerializer.Deserialize<UserDto>(response, _jsonOptions)!;
    }

    /// <summary>
    /// Updates an existing user using their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="request">The UserDto object containing the updated user information.</param>
    /// <returns>A Task representing the asynchronous update operation.</returns>
    /// <exception cref="Exception">Thrown when the update operation fails.</exception>
    public async Task UpdateAsync(int id, UserDto request) {
        var httpResponse = await client.PutAsJsonAsync($"{BaseUrl}/{id}", request);
        var response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) throw new Exception(response);
    }

    /// <summary>
    /// Deletes a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    /// <returns>A Task representing the asynchronous delete operation.</returns>
    /// <exception cref="Exception">Thrown when the delete operation fails.</exception>
    public async Task DeleteAsync(int id) {
        var httpResponse = await client.DeleteAsync($"{BaseUrl}/{id}");
        var responseContent = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) throw new Exception(responseContent);
    }
}