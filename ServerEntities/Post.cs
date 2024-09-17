using ServerEntities.Util;

namespace ServerEntities;

/// <summary>
/// Represents a post entity.
/// </summary>
/// <param name="authorId">The ID of the author of the post.</param>
/// <param name="title">The title of the post.</param>
/// <param name="content">The content of the post.</param>
/// <param name="channelId">The optional ID of the channel where the post is published.</param>
/// <param name="lang">The language of the post (default is "eng").</param>
/// <param name="image">The optional image associated with the post (default is an empty string).</param>
/// <param name="attachments">The optional attachments associated with the post (default is an empty string).</param>
/// <param name="tags">The optional tags associated with the post (default is an empty string).</param>
public class Post(int authorId, string title, string content, int? channelId = null, string lang = "eng", string image = "", string attachments = "", string tags = "")
    : IServerEntity {
    /// Gets or sets the unique identifier for the post.
    public int Id { get; set; } = -1;

    /// Gets the name of the entity, which is "Post" in this case.
    public string EntityName => "Post";

    /// Gets the ID of the author who created the post.
    public int AuthorId { get; } = authorId;

    /// Gets the ID of the channel associated with the post, if any.
    public int? ChannelId { get; } = channelId;

    /// Gets the language of the post.
    public string Lang { get; } = lang;

    /// Gets the title of the post.
    public string Title { get; } = title;

    /// Gets or sets the content of the post.
    public string Content { get; private set; } = content;

    /// Gets or sets the image associated with the post.
    public string Image { get; private set; } = image;

    /// Gets or sets the attachments associated with the post.
    public string Attachments { get; private set; } = attachments;

    /// Gets or sets the tags associated with the post.
    public string Tags { get; private set; } = tags;

    /// Gets or sets the date and time when the post was published.
    public DateTime PublishDate { get; set; }

    /// Gets or sets the date and time when the post was last modified.
    public DateTime LastModifiedDate { get; set; }

    /// Updates the content of the post and sets the last modified date to the current date and time.
    /// <param name="newContent">The new content to be associated with the post.</param>
    /// <return>The modified Post instance with updated content.</return>
    public Post ModifyContent(string newContent) {
        Content = newContent;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    /// Changes the image associated with the post and updates the last modified date.
    /// <param name="newImage">The new image to be associated with the post.</param>
    /// <return>The modified Post instance with updated image.</return>
    public Post ModifyImage(string newImage) {
        Image = newImage;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    /// Changes the attachments associated with the post and updates the last modified date.
    /// <param name="newAttachments">The new attachments to be associated with the post.</param>
    /// <return>The modified Post instance with updated attachments.</return>
    public Post ChangeAttachments(string newAttachments) {
        Attachments = newAttachments;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    /// Changes the tags associated with the post and updates the last modified date.
    /// <param name="newTags">The new tags to be associated with the post.</param>
    /// <return>The modified Post instance with updated tags.</return>
    public Post ChangeTags(string newTags) {
        Tags = newTags;
        LastModifiedDate = DateTime.Now;
        return this;
    }
}