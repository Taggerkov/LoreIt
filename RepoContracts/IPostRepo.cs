using ServerEntities;

namespace RepoContracts;

/// Interface representing the repository for managing posts.
public interface IPostRepo {
    /// Retrieves all posts available in the repository.
    /// <returns>An IQueryable containing all posts.</returns>
    IQueryable<Post> GetAllPosts();

    /// Retrieves all posts that are not associated with any channel.
    /// <returns>An IQueryable containing the posts without a channel.</returns>
    IQueryable<Post> GetAllWithoutChannel();

    /// Retrieves all posts associated with a specific channel.
    /// <param name="channelId">The unique identifier of the channel.</param>
    /// <returns>An IQueryable containing the posts from the specified channel.</returns>
    IQueryable<Post> GetAllFromChannel(int channelId);

    /// Retrieves a post asynchronously by its identifier.
    /// <param name="id">The unique identifier of the post to retrieve.</param>
    /// <returns>A Task containing the post if found, otherwise throws an exception.</returns>
    Task<Post> GetAsync(int id);

    /// Adds a new post asynchronously to the repository.
    /// <param name="post">The post object to be added.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task<Post> AddAsync(Post post);

    /// Updates a post asynchronously based on the given post object.
    /// <param name="post">The post object containing updated information.</param>
    /// <return>A Task representing the asynchronous operation.</return>
    Task UpdateAsync(Post post);

    /// Deletes a post asynchronously based on the given post identifier.
    /// <param name="id">The identifier of the post to be deleted.</param>
    /// <return>A Task representing the asynchronous operation.</return>
    Task DeleteAsync(int id);
}