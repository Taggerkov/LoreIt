using ServerEntities.Util;

namespace ServerEntities;

public class Post(int authorId, string title, int? channelId = null, string lang = "eng", string content = "", string image = "", string attachments = "", string tags = "")
    : IServerEntity {
    public int Id { get; set; } = -1;
    public string EntityName => "Post";
    public int AuthorId { get; } = authorId;
    public int? ChannelId { get; } = channelId;
    public string Lang { get; } = lang;
    public string Title { get; } = title;
    public string Content { get; private set; } = content;
    public string Image { get; private set; } = image;
    public string Attachments { get; private set; } = attachments;
    public string Tags { get; private set; } = tags;
    public DateTime PublishDate { get; set; }
    public DateTime LastModifiedDate { get; set; }

    public Post ModifyContent(string newContent) {
        Content = newContent;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    public Post ModifyImage(string newImage) {
        Image = newImage;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    public Post ChangeAttachments(string newAttachments) {
        Attachments = newAttachments;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    public Post ChangeTags(string newTags) {
        Tags = newTags;
        LastModifiedDate = DateTime.Now;
        return this;
    }
}