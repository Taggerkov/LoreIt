namespace APILibrary;

/// <summary>
/// Data transfer object representing a channel.
/// </summary>
public class ChannelDto {
    /// <summary>
    /// Gets or sets the unique identifier for the channel.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the owner of the channel.
    /// </summary>
    public int OwnerId { get; set; }

    /// <summary>
    /// Gets or sets the title of the channel.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the channel.
    /// </summary>
    public string Description { get; set; }
}