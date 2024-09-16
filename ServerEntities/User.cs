using System.Security.Cryptography;
using System.Text;
using ServerEntities.Util;

namespace ServerEntities;

/// Represents a User in the system.
public class User : IServerEntity {
    /// Gets or sets the unique identifier for the User.
    public int Id { get; set; } = -1;

    /// Gets the name of the entity.
    public string EntityName => "User";

    /// Gets or sets the username for the User.
    public string Username { get; private set; }

    /// Gets the salt used for hashing the User's password.
    private byte[] Salt { get; }

    /// Gets or sets the hashed representation of the User's password.
    private string PasswordHash { get; set; }

    /// Gets the email address of the User.
    public string? Email { get; private set; }

    /// Gets the tags that the User likes.
    public string? LikedTags { get; private set; }

    /// Gets or sets a value indicating whether the User has administrative privileges.
    public bool IsAdmin { get; set; } = false;

    /// Gets or sets the date when the User was published.
    public DateTime PublishDate { get; set; } = DateTime.Now;

    /// Gets or sets the date and time when the User was last modified.
    public DateTime LastModifiedDate { get; set; }

    /// User primary constructor.
    private User(string username, byte[] salt, string passwordHash, string? email = null, string? likedTags = null) {
        Username = username;
        Salt = salt;
        PasswordHash = passwordHash;
        Email = email;
        LikedTags = likedTags;
    }

    /// Creates a new User asynchronously.
    /// <param name="username">The username for the new user.</param>
    /// <param name="password">The password for the new user.</param>
    /// <param name="email">The optional email for the new user.</param>
    /// <param name="likedTags">The optional liked tags for the new user.</param>
    /// <return>A task that represents the asynchronous operation, containing the newly created User.</return>
    public static async Task<User> CreateAsync(string username, string password, string? email = null, string? likedTags = null) {
        var salt = await GenerateSaltAsync(32);
        var passwordHash = await HashPasswordAsync(password, await GenerateSaltAsync(32));
        return new User(username, salt, passwordHash, email, likedTags);
    }

    /// Changes the username of the User.
    /// <param name="newUsername">The new username to be set.</param>
    /// <return>The updated User object.</return>
    public User ChangeUsername(string newUsername) {
        Username = newUsername;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    /// Changes the password of the User.
    /// <param name="newPassword">The new password to be set.</param>
    /// <return>The updated User object.</return>
    public User ChangePassword(string newPassword) {
        PasswordHash = HashPasswordAsync(newPassword, Salt).Result;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    /// Changes the email address of the User.
    /// <param name="newEmail">The new email address to be set.</param>
    /// <return>The updated User object.</return>
    public User ChangeEmail(string newEmail) {
        Email = newEmail;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    /// Changes the liked tags of the User.
    /// <param name="newLikedTags">The new liked tags to be set.</param>
    /// <return>The updated User object.</return>
    public User ChangeLikedTags(string newLikedTags) {
        LikedTags = newLikedTags;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    /// Checks if the provided password matches the stored password hash.
    /// <param name="password">The plain text password to verify.</param>
    /// <return>True if the password matches the stored hash, otherwise false.</return>
    public bool CheckPassword(string password) {
        return PasswordHash == HashPasswordAsync(password, Salt).Result;
    }


    /// Generates a cryptographic salt asynchronously.
    /// <param name="length">The length of the salt in bytes.</param>
    /// <return>A task that represents the asynchronous operation, containing the generated salt as a byte array.</return>
    private static async Task<byte[]> GenerateSaltAsync(byte length) {
        var salt = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        await Task.Run(() => rng.GetBytes(salt));
        return salt;
    }

    /// Hashes a password using the specified salt asynchronously.
    /// <param name="password">The plain text password to hash.</param>
    /// <param name="salt">The cryptographic salt to use in the hashing process.</param>
    /// <return>A task that represents the asynchronous operation, containing the hashed password as a hexadecimal string.</return>
    private static async Task<string> HashPasswordAsync(string password, byte[] salt) {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var passwordWithSaltBytes = new byte[passwordBytes.Length + salt.Length];
        Buffer.BlockCopy(passwordBytes, 0, passwordWithSaltBytes, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, passwordWithSaltBytes, passwordBytes.Length, salt.Length);
        var hashBytes = await Task.Run(() => SHA256.HashData(passwordWithSaltBytes));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}