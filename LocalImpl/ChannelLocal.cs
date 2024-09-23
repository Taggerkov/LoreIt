using System.Text.Json;
using LocalImpl.Util;
using RepoContracts;
using ServerEntities;

namespace LocalImpl;

/// <summary>
/// Provides a local implementation of the IChannelRepo interface, managing Channel entities.
/// </summary>
public sealed class ChannelLocal : EntityLocal<Channel>, IChannelRepo {
    /// <summary>
    /// Represents the entity type used for the Channel class within the local implementation.
    /// This constant is utilized to specify the type of entity being operated on within the ChannelLocal class.
    /// </summary>
    private const string EntityType = "Channel";

    /// <summary>
    /// A private static field that holds the singleton instance of the ChannelLocal class.
    /// This instance is lazily initialized when the Get method is invoked for the first time.
    /// </summary>
    private static ChannelLocal? _instance;

    /// <summary>
    /// A private field that holds a list of Channel objects.
    /// This list is initialized by deserializing JSON data from a file specified by FilePaths.ChannelsPath.
    /// </summary>
    private List<Channel> _list = JsonSerializer.Deserialize<List<Channel>>(File.ReadAllText(FilePaths.ChannelsPath)) ?? [];

    /// <summary>
    /// Retrieves the singleton instance of the ChannelLocal class.
    /// </summary>
    /// <returns>The singleton instance of ChannelLocal.</returns>
    public static ChannelLocal Get() {
        if (_instance is null) return _instance = new ChannelLocal();
        return _instance;
    }

    /// <summary>
    /// A sealed class that provides local storage and operations for Channel entities.
    /// Implements the IChannelRepo interface for managing Channel data locally.
    /// </summary>
    private ChannelLocal() {
        Build(EntityType);
    }

    /// <summary>
    /// Retrieves all channels from the local collection.
    /// </summary>
    /// <returns>An IQueryable containing all channels in the local collection.</returns>
    public IQueryable<Channel> GetAll() {
        return _GetAll(_list);
    }

    /// <summary>
    /// Asynchronously retrieves the channel that matches the given ID from the local collection.
    /// </summary>
    /// <param name="id">The unique identifier of the channel to retrieve.</param>
    /// <returns>A task representing the asynchronous get operation. The task result contains the channel with the specified ID.</returns>
    public async Task<Channel> GetAsync(int id) {
        return await _GetAsync(_list, id);
    }

    /// <summary>
    /// Asynchronously adds the provided channel to the local collection.
    /// </summary>
    /// <param name="channel">The channel entity to be added.</param>
    /// <returns>A task representing the asynchronous add operation. The task result contains the added channel.</returns>
    public async Task<Channel> AddAsync(Channel channel) {
        _list = await _AddAsync(_list, channel);
        await Save();
        return channel;
    }

    /// <summary>
    /// Asynchronously updates a channel with the specified details.
    /// </summary>
    /// <param name="channel">The channel entity with updated information.</param>
    /// <returns>A task representing the asynchronous update operation.</returns>
    public async Task UpdateAsync(Channel channel) {
        _list = await _UpdateAsync(_list, channel);
        await Save();
    }

    /// <summary>
    /// Asynchronously deletes a channel with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the channel to delete.</param>
    /// <returns>A task representing the asynchronous delete operation.</returns>
    public async Task DeleteAsync(int id) {
        _list = await _DeleteAsync(_list, id);
        await Save();
    }

    /// <summary>
    /// Asynchronously saves the current list of channels to the file specified in the file paths.
    /// </summary>
    /// <returns>A task representing the asynchronous save operation.</returns>
    /// <exception cref="IOException">Thrown when the data could not be saved to the file.</exception>
    private async Task Save() {
        await _Save(_list, FilePaths.ChannelsPath);
    }
}