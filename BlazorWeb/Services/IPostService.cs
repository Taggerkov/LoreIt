using APILibrary;

namespace BlazorWeb.Services;

/// <summary>
/// Defines methods for managing posts.
/// </summary>
public interface IPostService {
    /// <summary>
    /// Retrieves all posts.
    /// </summary>
    /// <returns>An IQueryable collection of PostDto objects representing all posts.</returns>
    public IQueryable<PostDto>? GetAll();

    /// <summary>
    /// Retrieves all posts without filtering by channel.
    /// </summary>
    /// <returns>An IQueryable collection of PostDto objects representing all posts without any channel-specific filtering.</returns>
    public IQueryable<PostDto> GetAllWithoutChannel();

    /// <summary>
    /// Retrieves all posts from a specified channel.
    /// </summary>
    /// <param name="channelId">The ID of the channel from which to retrieve the posts.</param>
    /// <returns>An IQueryable collection of PostDto objects representing the posts from the specified channel.</returns>
    public IQueryable<PostDto> GetAllFromChannel(int channelId);

    /// <summary>
    /// Retrieves a post asynchronously by its ID.
    /// </summary>
    /// <param name="id">The ID of the post to retrieve.</param>
    /// <returns>A Task representing the asynchronous operation, with a PostDto object as the result containing the retrieved post data.</returns>
    public Task<PostDto> GetAsync(string id);

    /// <summary>
    /// Adds a new post asynchronously using the provided PostDto data.
    /// </summary>
    /// <param name="request">The PostDto object containing the data for the new post.</param>
    /// <returns>A Task representing the asynchronous operation, with a PostDto object as the result containing the added post data.</returns>
    public Task<PostDto> AddAsync(PostDto request);

    /// <summary>
    /// Updates the post with the specified ID asynchronously with the provided PostDto data.
    /// </summary>
    /// <param name="id">The ID of the post to update.</param>
    /// <param name="request">The PostDto object containing the updated post data.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public Task UpdateAsync(int id, PostDto request);

    /// <summary>
    /// Deletes the post with the specified ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the post to delete.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public Task DeleteAsync(int id);
}