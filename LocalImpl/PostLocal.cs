using System.Text.Json;
using LocalImpl.Util;
using RepoContracts;
using ServerEntities;

namespace LocalImpl;

/// <summary>
/// Manages the local operations for Post entities.
/// </summary>
public sealed class PostLocal : EntityLocal<Post>, IPostRepo {
    /// <summary>
    /// Represents the type of entity being managed by the PostLocal class.
    /// </summary>
    private const string EntityType = "Post";

    /// <summary>
    /// Holds the singleton instance of the PostLocal class.
    /// </summary>
    private static PostLocal? _instance;

    /// <summary>
    /// A private list holding Post entities, initially populated from a JSON file.
    /// </summary>
    private List<Post> _list = JsonSerializer.Deserialize<List<Post>>(File.ReadAllText(FilePaths.PostsPath)) ?? [];

    /// <summary>
    /// Retrieves a singleton instance of the PostLocal class.
    /// </summary>
    /// <returns>
    /// The single instance of PostLocal.
    /// </returns>
    public static PostLocal Get() {
        if (_instance is null) return _instance = new PostLocal();
        return _instance;
    }

    /// <summary>
    /// Local implementation of the post repository.
    /// </summary>
    private PostLocal() {
        Build(EntityType);
    }

    /// <summary>
    /// Retrieves all posts from the local data store.
    /// </summary>
    /// <returns>An IQueryable collection of all posts.</returns>
    public IQueryable<Post> GetAll() {
        return _GetAll(_list);
    }

    /// <summary>
    /// Retrieves all posts that are not assigned to any channel.
    /// </summary>
    /// <returns>An IQueryable of posts that do not belong to any channel.</returns>
    public IQueryable<Post> GetAllWithoutChannel() {
        return _list.Where(p => p.ChannelId == -1).AsQueryable();
    }

    /// <summary>
    /// Retrieves all posts from a specified channel.
    /// </summary>
    /// <param name="channelId">The identifier of the channel to get posts from.</param>
    /// <returns>An IQueryable of posts from the specified channel.</returns>
    public IQueryable<Post> GetAllFromChannel(int channelId) {
        return _list.Where(p => p.ChannelId == channelId).AsQueryable();
    }

    /// <summary>
    /// Asynchronously retrieves a post by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the post to be retrieved.</param>
    /// <returns>A task representing the asynchronous get operation, containing the post if found.</returns>
    public async Task<Post> GetAsync(int id) {
        return await _GetAsync(_list, id);
    }

    /// <summary>
    /// Asynchronously adds a new post to the repository.
    /// </summary>
    /// <param name="post">The post to be added.</param>
    /// <returns>A task representing the asynchronous add operation.</returns>
    public async Task<Post> AddAsync(Post post) {
        _list = await _AddAsync(_list, post);
        await Save();
        return post;
    }

    /// <summary>
    /// Asynchronously updates an existing post.
    /// </summary>
    /// <param name="post">The post with updated information to be saved.</param>
    /// <returns>A task representing the asynchronous update operation.</returns>
    public async Task UpdateAsync(Post post) {
        _list = await _UpdateAsync(_list, post);
        await Save();
    }

    /// <summary>
    /// Asynchronously deletes a post by its ID.
    /// </summary>
    /// <param name="id">The ID of the post to be deleted.</param>
    /// <returns>A task representing the asynchronous delete operation.</returns>
    public async Task DeleteAsync(int id) {
        _list = await _DeleteAsync(_list, id);
        await Save();
    }

    /// <summary>
    /// Asynchronously saves the current list of posts to the local file system.
    /// </summary>
    /// <returns>A task representing the asynchronous save operation.</returns>
    /// <exception cref="IOException">Thrown when the data could not be saved to the file.</exception>
    private async Task Save() {
        await _Save(_list, FilePaths.PostsPath);
    }
}