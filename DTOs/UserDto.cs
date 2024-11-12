namespace APILibrary;

/// <summary>
/// Represents a Data Transfer Object (DTO) for a User.
/// </summary>
public class UserDto {
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public string? Email { get; set; }
}