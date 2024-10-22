using APILibrary;
using Microsoft.AspNetCore.Mvc;
using RepoContracts;
using ServerEntities;

namespace WebAPI.Controllers;

/// <summary>
/// This class manages operations related to posts, including creating, updating, retrieving,
/// and deleting posts, as well as retrieving comments associated with a post.
/// </summary>
/// <param name="repo">Instance of IPostRepo to perform post-related operations.</param>
/// <param name="commentRepo">Instance of ICommentRepo to perform comment-related operations.</param>
[ApiController]
[Route("api/[controller]")]
public class PostController(IPostRepo repo, ICommentRepo commentRepo) : ControllerBase {

    /// Creates a new post based on the provided PostDto.
    /// <param name="post">An object containing the details of the post to create.</param>
    /// <return>An IActionResult representing the result of the create operation.</return>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostDto post) {
        var newComment = await repo.AddAsync(new Post(post.Id, post.Title, post.Content));
        return Created($"/Comment/{post.Id}", newComment);
    }

    /// Updates an existing post with the specified identifier.
    /// <param name="id">The unique identifier of the post to update.</param>
    /// <param name="post">An object containing the updated post information.</param>
    /// <return>An IActionResult representing the result of the update operation.</return>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PostDto post) {
        await repo.UpdateAsync(new Post(id, post.Title, post.Content));
        return NoContent();
    }

    /// Retrieves a single post based on its unique identifier.
    /// <param name="id">The unique identifier of the post to retrieve.</param>
    /// <return>An IActionResult containing the post that matches the specified identifier.</return>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id) {
        return Ok(await repo.GetAsync(id));
    }

    /// Retrieves multiple posts based on optional filters for title contains and user ID.
    /// <param name="titleContains">Optional filter that specifies the substring to search for within post titles.</param>
    /// <param name="userId">Optional filter that specifies the user ID to filter posts by author.</param>
    /// <return>An IActionResult containing a collection of posts that match the specified filters.</return>
    [HttpGet]
    public IActionResult GetMany([FromQuery] string titleContains, [FromQuery] int? userId) {
        var posts = repo.GetAll();
        if (!string.IsNullOrEmpty(titleContains)) {
            posts = posts.Where(p => p.Title.Contains(titleContains));
        }
        if (userId.HasValue) {
            posts = posts.Where(p => p.AuthorId == userId.Value);
        }
        return Ok(posts);
    }

    /// Deletes a specific post based on its unique identifier.
    /// <param name="id">The unique identifier of the post to be deleted.</param>
    /// <return>An IActionResult indicating the outcome of the delete operation.</return>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        await repo.DeleteAsync(id);
        return NoContent();
    }

    /// Retrieves all comments associated with a specific post.
    /// <param name="postId">The unique identifier of the post whose comments are to be retrieved.</param>
    /// <return>An IActionResult containing the collection of comments for the specified post.</return>
    [HttpGet("{postId}/comments")]
    public IActionResult GetCommentsForPost(int postId) {
        return Ok(commentRepo.GetAllFromPost(postId));
    }
}