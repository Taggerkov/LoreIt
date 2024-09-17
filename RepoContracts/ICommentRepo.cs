using ServerEntities;

namespace RepoContracts;

/// Provides methods for managing comments in the repository.
public interface ICommentRepo {
    /// Retrieves all comments available in the repository.
    /// <return>An IQueryable collection of all comments.</return>
    IQueryable<Comment> GetAllComments();

    /// Retrieves all comments associated with a specific post.
    /// <param name="postId">The unique identifier of the post whose comments are to be retrieved.</param>
    /// <return>An IQueryable collection of comments associated with the specified post.</return>
    IQueryable<Comment> GetAllFromPost(int postId);

    /// Retrieves a comment asynchronously by its ID.
    /// <param name="id">The unique identifier of the comment to be retrieved.</param>
    /// <return>A task that represents the asynchronous operation. The task result contains the retrieved comment.</return>
    Task<Comment> GetAsync(int id);

    /// Adds a new comment to the repository.
    /// <param name="comment">The comment object to be added.</param>
    /// <return>A task that represents the asynchronous operation. The task result contains the added comment.</return>
    Task<Comment> AddAsync(Comment comment);

    /// Updates an existing comment in the repository.
    /// <param name="comment">The comment object with updated information.</param>
    /// <return>A task that represents the asynchronous operation.</return>
    Task UpdateAsync(Comment comment);

    /// Deletes a comment from the repository.
    /// <param name="id">The identifier of the comment to be deleted.</param>
    /// <return>A task that represents the asynchronous operation.</return>
    Task DeleteAsync(int id);
}