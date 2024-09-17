using ServerEntities.Util;

namespace ServerEntities;

/// <summary>
/// Represents a channel entity.
/// </summary>
/// <param name="ownerId">The ID of the owner of the channel.</param>
/// <param name="title">The title of the channel.</param>
/// <param name="description">The optional description of the channel (default is null).</param>
/// <param name="rules">The optional rules of the channel (default is null).</param>
/// <param name="icon">The optional icon of the channel (default is null).</param>
public class Channel(int ownerId, string title, string? description = null, string? rules = null, string? icon = null) : IServerEntity {
    /// Gets or sets the unique identifier for this entity.
    /// This property is used to uniquely identify the entity within the system.
    public int Id { get; set; } = -1;

    /// Gets the entity type name.
    /// This property returns the type name of the entity as a string.
    /// For a Channel entity, it returns "Channel".
    public string EntityName => "Channel";

    /// Represents the unique identifier of the owner of the channel.
    /// This property holds the ID of the user who created the channel.
    /// It is assigned during the channel creation process and does not change.
    public int OwnerId { get; } = ownerId;

    /// Represents the title of the channel.
    /// This property holds the name of the channel.
    /// The title is assigned when the channel is created
    /// and serves as its primary identifier for users.
    public string Title { get; set; } = title;

    /// Represents the description of the channel.
    /// This property provides information about the channel,
    /// such as its purpose, content, or any other relevant details.
    /// It can be set when the channel is created and updated later as needed.
    public string? Description { get; set; } = description;

    /// Represents the rules associated with the channel.
    /// This property can contain guidelines and regulations that members of the channel must adhere to.
    /// It can be updated to reflect any changes in the channel’s policies or community standards.
    public string? Rules { get; set; } = rules;

    /// Represents the icon associated with the channel.
    /// This property can be used to store a URL or a path to the icon image.
    /// It can be updated to change the visual representation of the channel.
    public string? Icon { get; set; } = icon;

    /// Represents the date and time when the channel was first published.
    /// This property is set when the channel is initially created and remains constant throughout the channel's lifecycle.
    public DateTime PublishDate { get; set; } = DateTime.Now;

    /// Represents the date and time when the channel was last modified.
    /// This property is updated whenever a modification is made to the channel,
    /// such as changes in the channel's description, rules, or icon.
    public DateTime LastModifiedDate { get; set; }

    /// Modifies the description of the channel to the specified new description.
    /// <param name="newDescription">The new description to be assigned to the channel.</param>
    /// <return>The modified channel with the updated description.</return>
    public Channel ModifyDescription(string newDescription) {
        Description = newDescription;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    /// Modifies the rules of the channel to the specified new rules.
    /// <param name="newRules">The new rules to be assigned to the channel.</param> <return>The modified channel with the updated rules.</return>
    public Channel ModifyRules(string newRules) {
        Rules = newRules;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    /// Changes the icon of the channel to a new specified icon.
    /// <param name="newIcon">The new icon to be assigned to the channel.</param> <return>The modified channel with the updated icon.</return>
    public Channel ChangeIcon(string newIcon) {
        Icon = newIcon;
        LastModifiedDate = DateTime.Now;
        return this;
    }
}