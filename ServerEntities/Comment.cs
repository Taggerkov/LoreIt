using ServerEntities.Util;

namespace ServerEntities;

public class Comment(int postId, int commenterId, string content = "", string attachments = "") : IServerEntity {
    public int Id { get; set; } = -1;
    public string EntityName => "Comment";
    public int PostId { get; } = postId;
    public int CommenterId { get; } = commenterId;
    public string Content { get; private set; } = content;
    public string Attachments { get; private set; } = attachments;
    public DateTime PublishDate { get; set; } = DateTime.Now;
    public DateTime LastModifiedDate { get; set; }

    public Comment ModifyContent(string newContent) {
        Content = newContent;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    public Comment ChangeAttachments(string newAttachments) {
        Attachments = newAttachments;
        LastModifiedDate = DateTime.Now;
        return this;
    }
}