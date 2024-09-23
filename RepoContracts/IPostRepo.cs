using RepoContracts.Util;
using ServerEntities;

namespace RepoContracts;

/// Interface representing the repository for managing posts.
public interface IPostRepo : IServerEntityRepo<Post>{
    /// Retrieves all posts that are not associated with any channel.
    /// <returns>An IQueryable containing the posts without a channel.</returns>
    IQueryable<Post> GetAllWithoutChannel();

    /// Retrieves all posts associated with a specific channel.
    /// <param name="channelId">The unique identifier of the channel.</param>
    /// <returns>An IQueryable containing the posts from the specified channel.</returns>
    IQueryable<Post> GetAllFromChannel(int channelId);
}