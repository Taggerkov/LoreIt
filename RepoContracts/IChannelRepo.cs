using ServerEntities;

namespace RepoContracts;

/// Interface for channel repository operations.
public interface IChannelRepo {
    /// Retrieves all channels.
    /// <return>An IQueryable collection containing all channels.</return>
    IQueryable<Channel> GetAllChannels();

    /// Retrieves a channel asynchronously by its identifier.
    /// <param name="id">The identifier of the channel to retrieve.</param>
    /// <return>A task representing the asynchronous operation, with a result containing the retrieved channel.</return>
    Task<Channel> GetAsync(int id);

    /// Adds a new channel asynchronously.
    /// <param name="channel">The channel object to be added.</param>
    /// <return>A task representing the asynchronous operation, with a result containing the added channel.</return>
    Task<Channel> AddAsync(Channel channel);

    /// Updates an existing channel asynchronously.
    /// <param name="channel">The channel entity with updated information.</param>
    /// <return>A Task representing the asynchronous operation.</return>
    Task UpdateAsync(Channel channel);

    /// Deletes a channel asynchronously by its identifier.
    /// <param name="id">The identifier of the channel to delete.</param>
    /// <return>A Task representing the asynchronous operation.</return>
    Task DeleteAsync(int id);
}