using System.Text.Json;
using APILibrary;

namespace BlazorWeb.Services.HTTP;

/// <summary>
/// Service class for handling HTTP requests related to Posts.
/// </summary>
public class PostHttp(HttpClient client) : IPostService {
    /// <summary>
    /// Specifies the base URL of the Post API endpoints.
    /// </summary>
    private const string BaseUrl = "http://localhost:5298/api/Post";

    /// <summary>
    /// Configures the JSON serialization options to use when interacting with HTTP API endpoints.
    /// </summary>
    /// <remarks>
    /// The JSON options are set to allow case-insensitive property name matching during serialization and deserialization processes,
    /// making it easier to handle JSON data that may have varying case conventions.
    /// </remarks>
    private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    /// <summary>
    /// Retrieves all posts from the API.
    /// </summary>
    /// <returns>An IQueryable collection of PostDto objects representing all posts.</returns>
    /// <exception cref="Exception">Thrown when the retrieval operation fails.</exception>
    public IQueryable<PostDto>? GetAll() {
        var httpResponse = client.GetFromJsonAsync<IQueryable<PostDto>>(BaseUrl);
        if (httpResponse is null) throw new Exception("Failed to retrieve users.");
        return httpResponse.Result;
    }


    public IQueryable<PostDto> GetAllWithoutChannel() {
        throw new NotImplementedException();
    }

    public IQueryable<PostDto> GetAllFromChannel(int channelId) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Retrieves a post asynchronously by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the post to retrieve.</param>
    /// <returns>A Task representing the asynchronous operation, with a PostDto object as the result.</returns>
    /// <exception cref="Exception">Thrown when the retrieval operation fails.</exception>
    public async Task<PostDto> GetAsync(int id) {
        var httpResponse = await client.GetFromJsonAsync<PostDto>($"{BaseUrl}/{id}");
        if (httpResponse == null) throw new Exception("Failed to retrieve user.");
        return httpResponse;
    }

    /// <summary>
    /// Adds a new post to the API.
    /// </summary>
    /// <param name="request">The PostDto object containing the details of the post to be added.</param>
    /// <returns>A Task representing an asynchronous operation that returns the added PostDto.</returns>
    /// <exception cref="Exception">Thrown when the addition operation fails.</exception>
    public async Task<PostDto> AddAsync(PostDto request) {
        var httpResponse = await client.PostAsJsonAsync(BaseUrl, request);
        var response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) throw new Exception(response);
        return JsonSerializer.Deserialize<PostDto>(response, _jsonOptions)!;
    }

    /// <summary>
    /// Updates an existing post asynchronously based on the provided ID and request data.
    /// </summary>
    /// <param name="id">The unique identifier of the post to be updated.</param>
    /// <param name="request">The updated data for the post in a PostDto object.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="Exception">Thrown when the update operation fails.</exception>
    public async Task UpdateAsync(int id, PostDto request) {
        var httpResponse = await client.PutAsJsonAsync($"{BaseUrl}/{id}", request);
        var response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) throw new Exception(response);
    }

    /// <summary>
    /// Deletes a post by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the post to delete.</param>
    /// <exception cref="Exception">Thrown when the deletion operation fails.</exception>
    public async Task DeleteAsync(int id) {
        var httpResponse = await client.DeleteAsync($"{BaseUrl}/{id}");
        var responseContent = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) throw new Exception(responseContent);
    }
}