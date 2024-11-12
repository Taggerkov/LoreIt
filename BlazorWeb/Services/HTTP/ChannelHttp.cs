using System.Text.Json;
using APILibrary;

namespace BlazorWeb.Services.HTTP;

/// <summary>
/// Provides HTTP-based methods to interact with channel data.
/// </summary>
public class ChannelHttp(HttpClient client) : IChannelService {
    /// <summary>
    /// Represents the base URL of the API endpoints for channel-related operations.
    /// </summary>
    /// <remarks>
    /// This URL is used as the foundational URI for making HTTP requests to the Channel service's API.
    /// All specific endpoint paths are appended to this base URL to form the complete request URIs.
    /// </remarks>
    private const string BaseUrl = "http://localhost:5298/api/Channel";

    /// <summary>
    /// Configures the JSON serialization options to use when interacting with HTTP API endpoints.
    /// </summary>
    /// <remarks>
    /// The JSON options are set to allow case-insensitive property name matching during serialization and deserialization processes,
    /// making it easier to handle JSON data that may have varying case conventions.
    /// </remarks>
    private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    /// <summary>
    /// Retrieves all channel data.
    /// </summary>
    /// <returns>An IQueryable collection of ChannelDto representing all channels.</returns>
    /// <exception cref="Exception">Thrown when the retrieval operation fails.</exception>
    public IQueryable<ChannelDto>? GetAll() {
        var httpResponse = client.GetFromJsonAsync<IQueryable<ChannelDto>>(BaseUrl);
        if (httpResponse is null) throw new Exception("Failed to retrieve users.");
        return httpResponse.Result;
    }

    /// <summary>
    /// Retrieves a channel by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the channel to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved ChannelDto.</returns>
    /// <exception cref="Exception">Thrown when the retrieval operation fails.</exception>
    public async Task<ChannelDto> GetAsync(int id) {
        var httpResponse = await client.GetFromJsonAsync<ChannelDto>($"{BaseUrl}/{id}");
        if (httpResponse == null) throw new Exception("Failed to retrieve user.");
        return httpResponse;
    }

    /// <summary>
    /// Adds a new channel asynchronously.
    /// </summary>
    /// <param name="request">The ChannelDto object containing the data to be added.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains the added ChannelDto.</returns>
    /// <exception cref="Exception">Thrown when the add operation fails.</exception>
    public async Task<ChannelDto> AddAsync(ChannelDto request) {
        var httpResponse = await client.PostAsJsonAsync(BaseUrl, request);
        var response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) throw new Exception(response);
        return JsonSerializer.Deserialize<ChannelDto>(response, _jsonOptions)!;
    }

    /// <summary>
    /// Updates an existing channel with the provided data.
    /// </summary>
    /// <param name="id">The unique identifier of the channel to be updated.</param>
    /// <param name="request">The data transfer object containing updated channel information.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="Exception">Thrown when the update operation fails.</exception>
    public async Task UpdateAsync(int id, ChannelDto request) {
        var httpResponse = await client.PutAsJsonAsync($"{BaseUrl}/{id}", request);
        var response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) throw new Exception(response);
    }

    /// <summary>
    /// Deletes a channel asynchronously by its ID.
    /// </summary>
    /// <param name="id">The ID of the channel to be deleted.</param>
    /// <exception cref="Exception">Thrown when the delete operation fails.</exception>
    public async Task DeleteAsync(int id) {
        var httpResponse = await client.DeleteAsync($"{BaseUrl}/{id}");
        var responseContent = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode) throw new Exception(responseContent);
    }
}