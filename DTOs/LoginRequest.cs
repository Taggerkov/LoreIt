namespace APILibrary;

/// <summary>
/// Represents a request to log in to the API.
/// </summary>
/// <param name="username">The username of the user trying to log in.</param>
/// <param name="password">The password of the user trying to log in.</param>
public class LoginRequest(string username, string password) {
    public string Username { get; } = username;
    public string Password { get; } = password;
}