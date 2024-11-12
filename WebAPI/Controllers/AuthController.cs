using APILibrary;
using Microsoft.AspNetCore.Mvc;
using RepoContracts;
using LocalImpl;

namespace WebAPI.Controllers {
    /// <summary>
    /// Controller responsible for handling authentication-related actions.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase {
        /// <summary>
        /// Repository for handling user-related data operations.
        /// </summary>
        private readonly IUserRepo _userRepo = UserLocal.Get();

        /// <summary>
        /// Handles the user login by verifying the credentials.
        /// </summary>
        /// <param name="loginRequest">The login request containing the username and password.</param>
        /// <returns>The UserDto if the login is successful; otherwise, returns Unauthorized.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginRequest loginRequest) {
            try {
                var user = await _userRepo.GetByUsernameAsync(loginRequest.Username);
                if (user is null || user.CheckPassword(loginRequest.Password)) return Unauthorized();
                var userDto = new UserDto {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Password = string.Empty,
                };
                return Ok(userDto);
            }
            catch (InvalidOperationException) {
                return Unauthorized();
            }
        }
    }
}