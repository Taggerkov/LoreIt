using MemoryRepo.Util;
using RepoContracts;
using ServerEntities;
using ServerEntities.Util;

namespace MemoryRepo;

/// Implementation of post-related operations in memory repository.
public class PostImpl : ICrud, IPostRepo {
    /// <summary>
    /// Represents the collection of posts managed within the repository.
    /// Provides methods to interact with and manipulate post data.
    /// </summary>
    private List<Post> Posts { get; set; } = [];

    /// <summary>
    /// Represents the entity type for the Post implementation within the repository.
    /// Used for differentiating and managing entity-specific operations.
    /// </summary>
    private const string EntityType = "Post";

    /// Retrieves all posts available in the repository.
    /// <returns>An IQueryable containing all posts.</returns>
    public IQueryable<Post> GetAllPosts() {
        return Posts.AsQueryable();
    }

    /// Retrieves all posts that are not associated with any channel.
    /// <returns>An IQueryable containing the posts without a channel.</returns>
    public IQueryable<Post> GetAllWithoutChannel() {
        return Posts.Where(p => p.ChannelId == -1).AsQueryable();
    }

    /// Retrieves all posts associated with a specific channel.
    /// <param name="channelId">The unique identifier of the channel.</param>
    /// <returns>An IQueryable containing the posts from the specified channel.</returns>
    public IQueryable<Post> GetAllFromChannel(int channelId) {
        return Posts.Where(p => p.ChannelId == channelId).AsQueryable();
    }

    /// Retrieves a post asynchronously by its identifier.
    /// <param name="id">The unique identifier of the post to retrieve.</param>
    /// <returns>A Task containing the post if found, otherwise throws an exception.</returns>
    public Task<Post> GetAsync(int id) {
        if (ICrud.Read(GetGenericList(), id, EntityType) is Post post) return Task.FromResult(post);
        throw new Exception("Couldn't read Post or possible data corruption.");
    }

    /// Adds a new post asynchronously to the repository.
    /// <param name="post">The post object to be added.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public Task<Post> AddAsync(Post post) {
        Posts = ICrud.Create(GetGenericList(), post).Cast<Post>().ToList();
        return Task.FromResult(post);
    }

    /// Updates a post asynchronously based on the given post object.
    /// <param name="post">The post object containing updated information.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public Task UpdateAsync(Post post) {
        Posts = ICrud.Update(GetGenericList(), post).Cast<Post>().ToList();
        return Task.CompletedTask;
    }

    /// Deletes a post asynchronously based on the given post identifier.
    /// <param name="id">The identifier of the post to be deleted.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public Task DeleteAsync(int id) {
        Posts = ICrud.Delete(GetGenericList(), id, EntityType).Cast<Post>().ToList();
        return Task.CompletedTask;
    }

    /// Retrieves the list of posts as a generic collection of IServerEntity.
    /// <returns>
    /// An IEnumerable of IServerEntity representing the current list of posts. </returns>
    #pragma warning disable CA1859
    private IEnumerable<IServerEntity> GetGenericList() {
        return Posts;
    }
}