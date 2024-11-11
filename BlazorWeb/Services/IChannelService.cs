using APILibrary;

namespace BlazorWeb.Services;

/// <summary>
/// Provides functionalities to manage channels.
/// </summary>
public interface IChannelService {
    /// <summary>
    /// Retrieves all channels.
    /// </summary>
    /// <returns>An <see cref="IQueryable"/> sequence of channel data.</returns>
    public IQueryable<ChannelDto>? GetAll();

    /// <summary>
    /// Retrieves a channel asynchronously by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the channel to be retrieved.</param>
    /// <returns>A task representing the asynchronous operation, containing the retrieved channel data.</returns>
    public Task<ChannelDto> GetAsync(string id);

    /// <summary>
    /// Adds a new channel asynchronously.
    /// </summary>
    /// <param name="request">The data of the channel to be added.</param>
    /// <returns>A task representing the asynchronous operation, containing the added channel data.</returns>
    public Task<ChannelDto> AddAsync(ChannelDto request);

    /// <summary>
    /// Updates a channel asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the channel to be updated.</param>
    /// <param name="request">The channel data to be updated.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task UpdateAsync(int id, ChannelDto request);

    /// <summary>
    /// Deletes a channel asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the channel to be deleted.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task DeleteAsync(int id);
}