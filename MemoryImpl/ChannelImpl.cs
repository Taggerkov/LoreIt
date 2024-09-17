using MemoryRepo.Util;
using RepoContracts;
using ServerEntities;
using ServerEntities.Util;

namespace MemoryRepo;

/// Represents an implementation of the IChannelRepo interface providing CRUD operations for channels.
public class ChannelImpl : ICrud, IChannelRepo {
    /// The list of Channel entities managed by the repository.
    private List<Channel> Channels { get; set; } = [];

    /// The name of the entity type used in the CRUD operations for channels.
    private const string EntityType = "Channel";

    /// Retrieves all channels.
    /// <return>An IQueryable collection containing all channels.</return>
    public IQueryable<Channel> GetAllChannels() {
        return Channels.AsQueryable();
    }

    /// Retrieves a channel asynchronously by its identifier.
    /// <param name="id">The identifier of the channel to retrieve.</param>
    /// <return>A task representing the asynchronous operation, with a result containing the retrieved channel.</return>
    public Task<Channel> GetAsync(int id) {
        if (ICrud.Read(GetGenericList(), id, EntityType) is Channel channel) return Task.FromResult(channel);
        throw new Exception("Couldn't read Channel or possible data corruption.");
    }

    /// Adds a new channel asynchronously.
    /// <param name="channel">The channel object to be added.</param>
    /// <return>A task representing the asynchronous operation, with a result containing the added channel.</return>
    public Task<Channel> AddAsync(Channel channel) {
        Channels = ICrud.Create(GetGenericList(), channel).Cast<Channel>().ToList();
        return Task.FromResult(channel);
    }

    /// Updates an existing channel asynchronously.
    /// <param name="channel">The channel entity with updated information.</param>
    /// <return>A Task representing the asynchronous operation.</return>
    public Task UpdateAsync(Channel channel) {
        Channels = ICrud.Update(GetGenericList(), channel).Cast<Channel>().ToList();
        return Task.CompletedTask;
    }

    /// Deletes a channel asynchronously by its identifier. <param name="id">The identifier of the channel to delete.</param> <return>A Task representing the asynchronous operation.</return>
    public Task DeleteAsync(int id) {
        Channels = ICrud.Delete(GetGenericList(), id, EntityType).Cast<Channel>().ToList();
        return Task.CompletedTask;
    }

    /// Returns a generic list of IServerEntity items.
    /// <returns>An IEnumerable of IServerEntity items representing the current collection of channels.</returns>
    #pragma warning disable CA1859
    private IEnumerable<IServerEntity> GetGenericList() {
        return Channels;
    }
}