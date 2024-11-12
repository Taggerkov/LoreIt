using APILibrary;

namespace BlazorWeb.Services;

/// <summary>
/// Defines the functionality for interacting with comments in the application.
/// </summary>
public interface ICommentService {
    /// <summary>
    /// Retrieves all comments.
    /// </summary>
    /// <returns>A queryable collection of all comments.</returns>
    public IQueryable<CommentDto>? GetAll();

    /// <summary>
    /// Retrieves all comments associated with a specific post.
    /// </summary>
    /// <param name="postId">The ID of the post to retrieve comments for.</param>
    /// <returns>A queryable collection of comments associated with the specified post.</returns>
    public IQueryable<CommentDto> GetAllFromPost(int postId);

    /// <summary>
    /// Retrieves a comment asynchronously based on the provided comment ID.
    /// </summary>
    /// <param name="id">The ID of the comment to retrieve.</param>
    /// <returns>A task representing the async retrieval operation, with the retrieved comment.</returns>
    public Task<CommentDto> GetAsync(int id);

    /// <summary>
    /// Adds a new comment asynchronously.
    /// </summary>
    /// <param name="request">The data for the new comment.</param>
    /// <returns>A task representing the async add operation, with the newly created comment.</returns>
    public Task<CommentDto> AddAsync(CommentDto request);

    /// <summary>
    /// Updates an existing comment asynchronously based on the provided comment ID.
    /// </summary>
    /// <param name="id">The ID of the comment to update.</param>
    /// <param name="request">The new data for the comment.</param>
    /// <returns>A task representing the async update operation.</returns>
    public Task UpdateAsync(int id, CommentDto request);

    /// <summary>
    /// Deletes a comment asynchronously based on the provided comment ID.
    /// </summary>
    /// <param name="id">The ID of the comment to delete.</param>
    /// <returns>A task representing the async delete operation.</returns>
    public Task DeleteAsync(int id);
}