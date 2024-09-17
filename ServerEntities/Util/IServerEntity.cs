namespace ServerEntities.Util;

/// Represents a generic server entity that serves as a base for different types of entities
/// in the server-side application.
public interface IServerEntity {
    /// Gets or sets the unique identifier for the entity.
    /// This property is used to distinguish each entity instance within the server's context.
    int Id { get; set; }

    /// Gets the name of the entity.
    /// This property returns a string representing the type or category of the entity,
    /// which helps identify the entity within the server's context.
    string EntityName { get; }

    /// Gets or sets the date and time when the entity was published.
    /// This property indicates the initial publish timestamp of the entity,
    /// providing a means to track the entity's creation time or first appearance.
    DateTime PublishDate { get; set; }

    /// Gets or sets the date and time when the entity was last modified.
    /// This property indicates the last update timestamp of the entity,
    /// providing a means to track changes over time.
    DateTime LastModifiedDate { get; set; }
}