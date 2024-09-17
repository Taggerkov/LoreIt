using MemoryRepo.Util;
using RepoContracts;
using ServerEntities;
using ServerEntities.Util;

namespace MemoryRepo;

/// Represents a comment repository implementing ICrud and ICommentRepo interfaces.
public class CommentImpl : ICrud, ICommentRepo {
    /// <summary>
    /// Holds the collection of comments in the repository.
    /// Provides methods for managing comments, including adding, updating, and deleting comments.
    /// </summary>
    private List<Comment> Comments { get; set; } = [];

    /// <summary>
    /// Represents the type of entity being operated on in the Comment repository.
    /// Used as a constant identifier in CRUD operations.
    /// </summary>
    private const string EntityType = "Comment";

    /// Retrieves all comments available in the repository.
    /// <return>An IQueryable collection of all comments.</return>
    public IQueryable<Comment> GetAllComments() {
        return Comments.AsQueryable();
    }

    /// Retrieves all comments associated with a specific post.
    /// <param name="postId">The unique identifier of the post whose comments are to be retrieved.</param>
    /// <return>An IQueryable collection of comments associated with the specified post.</return>
    public IQueryable<Comment> GetAllFromPost(int postId) {
        try {
            return Comments.Where(c => c.PostId == postId).AsQueryable();
        }
        catch (ArgumentNullException) {
            throw new InvalidOperationException($"Post ({postId}) does not exist or has no comments!");
        }
    }

    /// Retrieves a comment asynchronously by its ID.
    /// <param name="id">The unique identifier of the comment to be retrieved.</param>
    /// <return>A task that represents the asynchronous operation. The task result contains the retrieved comment.</return>
    public Task<Comment> GetAsync(int id) {
        if (ICrud.Read(GetGenericList(), id, EntityType) is Comment comment) return Task.FromResult(comment);
        throw new Exception("Couldn't read Comment or possible data corruption.");
    }

    /// Adds a new comment to the repository.
    /// <param name="comment">The comment object to be added.</param>
    /// <return>A task that represents the asynchronous operation. The task result contains the added comment.</return>
    public Task<Comment> AddAsync(Comment comment) {
        Comments = ICrud.Create(GetGenericList(), comment).Cast<Comment>().ToList();
        return Task.FromResult(comment);
    }

    /// Updates an existing comment in the repository.
    /// <param name="comment">The comment object with updated information.</param>
    /// <return>A task that represents the asynchronous operation.</return>
    public Task UpdateAsync(Comment comment) {
        Comments = ICrud.Update(GetGenericList(), comment).Cast<Comment>().ToList();
        return Task.CompletedTask;
    }

    /// Deletes a comment from the repository.
    /// <param name="id">The identifier of the comment to be deleted.</param>
    /// <return>A task that represents the asynchronous operation.</return>
    public Task DeleteAsync(int id) {
        Comments = ICrud.Delete(GetGenericList(), id, EntityType).Cast<Comment>().ToList();
        return Task.CompletedTask;
    }

    /// Retrieves a generic list of server entities.
    /// <return>An enumerable list of server entities.</return>
    #pragma warning disable CA1859
    private IEnumerable<IServerEntity> GetGenericList() {
        return Comments;
    }
}