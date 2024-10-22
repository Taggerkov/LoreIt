using APILibrary;
using Microsoft.AspNetCore.Mvc;
using RepoContracts;
using ServerEntities;

namespace WebAPI.Controllers;

/// <summary>
/// Controller responsible for handling operations related to comments.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CommentController(ICommentRepo repo) : ControllerBase {

    /// <summary>
    /// Creates a new comment based on the provided data transfer object.
    /// </summary>
    /// <param name="comment">The data transfer object containing the details of the comment to be created.</param>
    /// <returns>An IActionResult indicating the result of the create operation, including the created comment.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CommentDto comment) {
        var newComment = await repo.AddAsync(new Comment(comment.Id, comment.CommenterId, comment.Content));
        return Created($"/Comment/{comment.Id}", newComment);
    }

    /// <summary>
    /// Updates an existing comment identified by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the comment to update.</param>
    /// <param name="comment">The updated comment details.</param>
    /// <returns>An IActionResult indicating the result of the update operation.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CommentDto comment) {
        await repo.UpdateAsync(new Comment(id, comment.CommenterId, comment.Content));
        return NoContent();
    }

    /// <summary>
    /// Retrieves a single comment based on its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the comment to retrieve.</param>
    /// <returns>An IActionResult containing the requested comment.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id) {
        return Ok(await repo.GetAsync(id));
    }

    /// <summary>
    /// Retrieves multiple comments based on optional filtering criteria.
    /// </summary>
    /// <param name="userId">Optional User identifier to filter comments by commenter id.</param>
    /// <param name="postId">Optional Post identifier to filter comments by post id.</param>
    /// <returns>An IActionResult containing the filtered list of comments.</returns>
    [HttpGet]
    public IActionResult GetMany([FromQuery] int? userId, [FromQuery] int? postId) {
        var comments = repo.GetAll();
        if (userId.HasValue) {
            comments = comments.Where(c => c.CommenterId == userId);
        }
        if (postId.HasValue) {
            comments = comments.Where(c => c.PostId == postId.Value);
        }
        return Ok(comments);
    }

    /// <summary>
    /// Deletes a comment based on its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the comment to be deleted.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        await repo.DeleteAsync(id);
        return NoContent();
    }
}