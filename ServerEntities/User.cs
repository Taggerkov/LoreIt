using ServerEntities.Util;

namespace ServerEntities;

public class User(string username, string password, string email, string likedTags) : IServerEntity {
    public int Id { get; set; } = -1;
    public string EntityName => "User";
    public string Username { get; private set; } = username;
    public string Password { get; private set; } = password;
    public string Email { get; private set; } = email;
    public string LikedTags { get; private set; } = likedTags;
    public bool IsAdmin { get; set; } = false;
    public DateTime PublishDate { get;  set; } = DateTime.Now;
    public DateTime LastModifiedDate { get; set; }

    public User ChangeUsername(string newUsername) {
        Username = newUsername;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    public User ChangePassword(string newPassword) {
        Password = newPassword;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    public User ChangeEmail(string newEmail) {
        Email = newEmail;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    public User ChangeLikedTags(string newLikedTags) {
        LikedTags = newLikedTags;
        LastModifiedDate = DateTime.Now;
        return this;
    }
}