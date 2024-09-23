using System.Text.Json;
using LocalImpl.Util;
using RepoContracts;
using ServerEntities;

namespace LocalImpl;

/// Represents a local storage implementation for managing comments.
public sealed class CommentLocal : EntityLocal<Comment>, ICommentRepo {
    /// <summary>
    /// Specifies the type of entity managed by the CommentLocal class.
    /// </summary>
    /// <remarks>
    /// This constant holds the entity type as a string value, used to identify and categorize
    /// the specific type of entities the CommentLocal class is intended to manage.
    /// </remarks>
    private const string EntityType = "Channel";

    /// <summary>
    /// Singleton instance of the CommentLocal class.
    /// </summary>
    /// <remarks>
    /// This static field holds the only instance of the CommentLocal class,
    /// ensuring that only one instance exists throughout the application's lifecycle.
    /// It is utilized to manage and access comment-related functionalities in a local context.
    /// </remarks>
    private static CommentLocal? _instance;

    /// <summary>
    /// A list containing instances of the Comment class.
    /// </summary>
    /// <remarks>
    /// This list is initialized by deserializing JSON data from a file specified in the FilePaths.ChannelsPath.
    /// It is used to store and manage comments in a local context.
    /// </remarks>
    private List<Comment> _list = JsonSerializer.Deserialize<List<Comment>>(File.ReadAllText(FilePaths.ChannelsPath)) ?? [];

    /// Retrieves an instance of the CommentLocal class, creating it if it does not already exist.
    /// <return>An instance of the CommentLocal class.</return>
    public static CommentLocal Get() {
        if (_instance is null) return _instance = new CommentLocal();
        return _instance;
    }

    /// Provides local data access functionality for Comment entities.
    private CommentLocal() {
        Build(EntityType);
    }

    /// Retrieves all comments from the local storage.
    /// <returns>An IQueryable of all comments.</returns>
    public IQueryable<Comment> GetAll() {
        return _GetAll(_list);
    }

    /// Retrieves all comments associated with the specified post ID.
    /// <param name="postId">The ID of the post whose comments need to be retrieved.</param>
    /// <returns>An IQueryable of comments associated with the specified post ID.</returns>
    public IQueryable<Comment> GetAllFromPost(int postId) {
        try {
            return _list.Where(c => c.PostId == postId).AsQueryable();
        }
        catch (ArgumentNullException) {
            throw new InvalidOperationException($"{EntityType} ({postId}) does not exist or has no comments!");
        }
    }

    /// Asynchronously retrieves a comment by its ID.
    /// <param name="id">The ID of the comment to retrieve.</param>
    /// <returns>A Task representing the asynchronous operation, with the retrieved comment entity.</returns>
    public async Task<Comment> GetAsync(int id) {
        return await _GetAsync(_list, id);
    }

    /// Asynchronously adds a comment.
    /// <param name="comment">The comment entity to add.</param>
    /// <returns>A Task representing the asynchronous operation, with the added comment entity.</returns>
    /// <exception cref="IOException">Thrown when the data could not be saved to the file.</exception>
    public async Task<Comment> AddAsync(Comment comment) {
        _list = await _AddAsync(_list, comment);
        await Save();
        return comment;
    }

    /// Asynchronously updates a comment.
    /// <param name="comment">The comment entity to update.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="IOException">Thrown when the data could not be saved to the file.</exception>
    public async Task UpdateAsync(Comment comment) {
        _list = await _UpdateAsync(_list, comment);
        await Save();
    }

    /// Deletes a comment asynchronously based on the provided identifier.
    /// <param name="id">The identifier of the comment to be deleted.</param>
    /// <return>A task that represents the asynchronous delete operation.</return>
    /// <exception cref="IOException">Thrown when the delete operation could not complete due to an I/O error.</exception>
    public async Task DeleteAsync(int id) {
        _list = await _DeleteAsync(_list, id);
        await Save();
    }

    /// Asynchronously saves the current list of comments to a local file.
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="IOException">Thrown when the data could not be saved to the file.</exception>
    private async Task Save() {
        await _Save(_list, FilePaths.CommentsPath);
    }
}