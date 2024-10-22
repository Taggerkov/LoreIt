using APILibrary;
using Microsoft.AspNetCore.Mvc;
using RepoContracts;

namespace WebAPI.Controllers;

/// <summary>
/// Controller for handling user-related operations in the Web API.
/// </summary>
/// <param name="repo">The repository in charge of User data,</param>
[ApiController]
[Route("api/[controller]")]
public class UserController(IUserRepo repo) : ControllerBase {

    /// <summary>
    /// Handles the creation of a new user.
    /// </summary>
    /// <param name="user">The User Data Transfer Object containing user details.</param>
    /// <returns>An IActionResult indicating the result of the creation operation.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto user) {
        var newComment = await repo.AddAsync(ServerEntities.User.CreateAsync(user.Username, user.Password, user.Email).Result);
        return Created($"/Comment/{user.Id}", newComment);
    }

    /// <summary>
    /// Updates an existing user's details.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="user">The User Data Transfer Object containing updated user details.</param>
    /// <returns>An IActionResult indicating the result of the update operation.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserDto user) {
        var ext = await repo.GetAsync(id);
        ext.ChangeUsername(user.Username);
        ext.ChangeEmail(user.Email);
        await repo.UpdateAsync(ext);
        return NoContent();
    }

    /// <summary>
    /// Retrieves a single user's details by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <returns>An IActionResult containing the user's details.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id) {
        return Ok(await repo.GetAsync(id));
    }

    /// <summary>
    /// Retrieves a collection of users whose usernames contain the specified string.
    /// </summary>
    /// <param name="usernameContains">The substring to search for within usernames.</param>
    /// <returns>An IActionResult containing the list of users matching the criteria.</returns>
    [HttpGet]
    public IActionResult GetMany([FromQuery] string usernameContains) {
        return Ok(repo.GetAll());
    }

    /// <summary>
    /// Deletes a user based on their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to be deleted.</param>
    /// <returns>An IActionResult indicating the result of the deletion operation.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        await repo.DeleteAsync(id);
        return NoContent();
    }
}