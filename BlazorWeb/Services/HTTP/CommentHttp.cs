using System.Text.Json;
using APILibrary;

namespace BlazorWeb.Services.HTTP;

public class CommentHttp(HttpClient client) : ICommentService{
    /// <summary>
    /// The base URL for accessing the Comment API endpoints.
    /// </summary>
    /// <remarks>
    /// This URL is used as the root address for all HTTP requests made to the Comment API, enabling consistent and centralized configuration of the API endpoint.
    /// </remarks>
    private const string BaseUrl = "http://localhost:5298/api/Comment";

    /// <summary>
    /// Configures the JSON serialization options to use when interacting with HTTP API endpoints.
    /// </summary>
    /// <remarks>
    /// The JSON options are set to allow case-insensitive property name matching during serialization and deserialization processes,
    /// making it easier to handle JSON data that may have varying case conventions.
    /// </remarks>
    private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    /// <summary>
    /// Retrieves all comments.
    /// </summary>
    /// <returns>An <see cref="IQueryable{CommentDto}"/> representing all comments.</returns>
    /// <exception cref="Exception">Thrown when the retrieval operation fails.</exception>
    public IQueryable<CommentDto>? GetAll() {
        var httpResponse = client.GetFromJsonAsync<IQueryable<CommentDto>>(BaseUrl);
        if (httpResponse is null) throw new Exception("Failed to retrieve users.");
        return httpResponse.Result;
    }

    public IQueryable<CommentDto> GetAllFromPost(int postId) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Retrieves a specific comment by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the comment to retrieve.</param>
    /// <returns>A <see cref="Task{CommentDto}"/> representing the asynchronous operation, with a <see cref="CommentDto"/> result containing the comment data.</returns>
    /// <exception cref="Exception">Thrown when the retrieval operation fails.</exception>
    public async Task<CommentDto> GetAsync(int id) {
        var httpResponse = await client.GetFromJsonAsync<CommentDto>($"{BaseUrl}/{id}");
        if (httpResponse == null) throw new Exception("Failed to retrieve user.");
        return httpResponse;
    }

    /// <summary>
    /// Adds a new comment asynchronously.
    /// </summary>
    /// <param name="request">The <see cref="CommentDto"/> object representing the comment to add.</param>
    /// <returns>A <see cref="Task{CommentDto}"/> representing the asynchronous operation. The result contains the added <see cref="CommentDto"/>.</returns>
    /// <exception cref="Exception">Thrown if the add operation fails.</exception>
    public async Task<CommentDto> AddAsync(CommentDto request) {
        var httpResponse = await client.PostAsJsonAsync(BaseUrl, request);
        var response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) throw new Exception(response);
        return JsonSerializer.Deserialize<CommentDto>(response, _jsonOptions)!;
    }

    /// <summary>
    /// Updates an existing comment with the provided data.
    /// </summary>
    /// <param name="id">The unique identifier of the comment to be updated.</param>
    /// <param name="request">A <see cref="CommentDto"/> representing the updated comment data.</param>
    /// <exception cref="Exception">Thrown when the update operation fails.</exception>
    public async Task UpdateAsync(int id, CommentDto request) {
        var httpResponse = await client.PutAsJsonAsync($"{BaseUrl}/{id}", request);
        var response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) throw new Exception(response);
    }

    /// <summary>
    /// Deletes a comment by its ID.
    /// </summary>
    /// <param name="id">The ID of the comment to be deleted.</param>
    /// <exception cref="Exception">Thrown when the deletion operation fails.</exception>
    public async Task DeleteAsync(int id) {
        var httpResponse = await client.DeleteAsync($"{BaseUrl}/{id}");
        var responseContent = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) throw new Exception(responseContent);
    }
}