using ServerEntities.Util;

namespace ServerEntities;

/// <summary>
/// Represents a comment entity.
/// </summary>
/// <param name="postId">The ID of the post on which the comment is made.</param>
/// <param name="commenterId">The ID of the commenter.</param>
/// <param name="content">The content of the comment.</param>
/// <param name="attachments">The optional attachments associated with the comment (default is an empty string).</param>
public class Comment(int postId, int commenterId, string content, string attachments = "") : IServerEntity {
    /// Gets or sets the identifier of the comment.
    /// The Id is a unique integer that represents the comment within the system.
    public int Id { get; set; } = -1;

    /// Gets the name of the entity.
    /// This property returns a string that represents the entity as "Comment".
    /// It is a read-only property and cannot be modified.
    public string EntityName => "Comment";

    /// Gets the ID of the post.
    /// This property holds the unique identifier of the post associated with the comment.
    /// It is initialized with the provided postId and cannot be modified directly.
    public int PostId { get; } = postId;

    /// Gets the ID of the commenter.
    /// This property holds the unique identifier of the user who made the comment.
    /// It is initialized with the provided commenterId and cannot be modified directly.
    public int CommenterId { get; } = commenterId;

    /// Gets or sets the content of the comment.
    /// This property holds the main text or message of the comment.
    /// It is initialized with the provided content and cannot be modified directly.
    public string Content { get; private set; } = content;

    /// Gets or sets the attachments associated with the comment.
    /// These attachments can include files, links, or other supplementary data.
    /// The property is initialized to an empty string if no attachments are provided.
    public string Attachments { get; private set; } = attachments;

    /// Gets or sets the date and time when the comment was published.
    /// This property is set to the current date and time at the moment of comment creation.
    public DateTime PublishDate { get; set; } = DateTime.Now;

    /// Gets or sets the date and time when the comment was last modified.
    /// This property is updated whenever the comment's content or attachments are changed.
    public DateTime LastModifiedDate { get; set; }

    /// Modifies the content of an existing comment.
    /// <param name="newContent">The new content to be set for the comment.</param>
    /// <return>Returns the modified comment object with updated content.</return>
    public Comment ModifyContent(string newContent) {
        Content = newContent;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    /// Changes the attachments of an existing comment.
    /// <param name="newAttachments">The new attachments for the comment.</param>
    /// <return>Returns the modified comment object with updated attachments.</return>
    public Comment ChangeAttachments(string newAttachments) {
        Attachments = newAttachments;
        LastModifiedDate = DateTime.Now;
        return this;
    }
}