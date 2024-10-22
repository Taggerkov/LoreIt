using APILibrary;
using Microsoft.AspNetCore.Mvc;
using RepoContracts;
using ServerEntities;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController(IPostRepo repo, ICommentRepo commentRepo) : ControllerBase {

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostDto post) {
        var newComment = await repo.AddAsync(new Post(post.Id, post.Title, post.Content));
        return Created($"/Comment/{post.Id}", newComment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PostDto post) {
        await repo.UpdateAsync(new Post(id, post.Title, post.Content));
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id) {
        return Ok(await repo.GetAsync(id));
    }

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        await repo.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{postId}/comments")]
    public IActionResult GetCommentsForPost(int postId) {
        return Ok(commentRepo.GetAllFromPost(postId));
    }
}