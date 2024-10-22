using APILibrary;
using Microsoft.AspNetCore.Mvc;
using RepoContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserRepo repo) : ControllerBase {

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto user) {
        var newComment = await repo.AddAsync(ServerEntities.User.CreateAsync(user.Username, user.Password, user.Email).Result);
        return Created($"/Comment/{user.Id}", newComment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserDto user) {
        var ext = await repo.GetAsync(id);
        ext.ChangeUsername(user.Username);
        ext.ChangeEmail(user.Email);
        await repo.UpdateAsync(ext);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id) {
        return Ok(await repo.GetAsync(id));
    }

    [HttpGet]
    public IActionResult GetMany([FromQuery] string usernameContains) {
        return Ok(repo.GetAll());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        await repo.DeleteAsync(id);
        return NoContent();
    }
}