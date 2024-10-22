using APILibrary;
using Microsoft.AspNetCore.Mvc;
using RepoContracts;
using ServerEntities;

namespace WebAPI.Controllers;

/// <summary>
/// Controller for managing channels in the application.
/// Provides endpoints to create, update, retrieve single channel, retrieve multiple channels, and delete channels.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ChannelController(IChannelRepo repo, IPostRepo postRepo) : ControllerBase {
    
    /// <summary>
    /// Creates a new channel with the provided details.
    /// </summary>
    /// <param name="dto">The data transfer object containing the details for the new channel.</param>
    /// <returns>An IActionResult with the result of the creation operation.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ChannelDto dto) {
        var newComment = await repo.AddAsync(new Channel(dto.OwnerId, dto.Title, dto.Description));
        return Created($"/Comment/{dto.Id}", newComment);
    }

    /// <summary>
    /// Updates an existing channel with the given details.
    /// </summary>
    /// <param name="id">The unique identifier of the channel to update.</param>
    /// <param name="dto">The channel data transfer object containing the updated details.</param>
    /// <returns>An IActionResult indicating the result of the update operation.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ChannelDto dto) {
        await repo.UpdateAsync(new Channel(dto.OwnerId, dto.Title, dto.Description));
        return NoContent();
    }

    /// <summary>
    /// Retrieves a single channel by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the channel to retrieve.</param>
    /// <returns>An IActionResult containing the retrieved channel.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id) {
        return Ok(await repo.GetAsync(id));
    }

    /// <summary>
    /// Retrieves a list of all channels optionally filtered by a username substring.
    /// </summary>
    /// <param name="usernameContains">A substring to filter the channels by username, can be null.</param>
    /// <returns>An IActionResult containing the list of channels.</returns>
    [HttpGet]
    public IActionResult GetMany([FromQuery] string usernameContains) {
        return Ok(repo.GetAll());
    }

    /// <summary>
    /// Deletes a channel based on its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the channel to be deleted.</param>
    /// <returns>A task representing the asynchronous delete operation.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        await repo.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Retrieves all posts from a specific channel.
    /// </summary>
    /// <param name="postId">The unique identifier of the channel whose posts are to be retrieved.</param>
    /// <returns>An IActionResult containing the posts from the specified channel.</returns>
    [HttpGet("{id}/posts")]
    public IActionResult GetPostsFromChannel(int postId) {
        return Ok(postRepo.GetAllFromChannel(postId));
    }
}