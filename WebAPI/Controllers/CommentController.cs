using APILibrary;
using Microsoft.AspNetCore.Mvc;
using RepoContracts;
using ServerEntities;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController(ICommentRepo repo) : ControllerBase {

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CommentDto comment) {
        var newComment = await repo.AddAsync(new Comment(comment.Id, comment.CommenterId, comment.Content));
        return Created($"/Comment/{comment.Id}", newComment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CommentDto comment) {
        await repo.UpdateAsync(new Comment(id, comment.CommenterId, comment.Content));
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id) {
        return Ok(await repo.GetAsync(id));
    }

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        await repo.DeleteAsync(id);
        return NoContent();
    }
}