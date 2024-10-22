namespace APILibrary;

/// <summary>
/// Represents a Data Transfer Object (DTO) for Post data.
/// </summary>
public class PostDto {
    /// <summary>
    /// Gets or sets the unique identifier for the post.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the post.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the textual content of the post.
    /// </summary>
    public string Content { get; set; }
}