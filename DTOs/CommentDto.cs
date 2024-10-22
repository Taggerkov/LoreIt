namespace APILibrary;

/// <summary>
/// Data Transfer Object representing a comment.
/// </summary>
public class CommentDto {
    /// <summary>
    /// Gets or sets the unique identifier for the comment.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who made the comment.
    /// </summary>
    public int CommenterId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the post associated with the comment.
    /// </summary>
    public int PostId { get; set; }

    /// <summary>
    /// Gets or sets the content of the comment.
    /// </summary>
    public string Content { get; set; }
}