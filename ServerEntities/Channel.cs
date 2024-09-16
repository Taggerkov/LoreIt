using ServerEntities.Util;

namespace ServerEntities;

public class Channel(int ownerId, string title, string? description = null, string? rules = null, string? icon = null) : IServerEntity {
    public int Id { get; set; } = -1;
    public string EntityName => "Channel";
    public int OwnerId { get; } = ownerId;
    public string Title { get; set; } = title;
    public string? Description { get; set; } = description;
    public string? Rules { get; set; } = rules;
    public string? Icon { get; set; } = icon;
    public DateTime PublishDate { get; set; } = DateTime.Now;
    public DateTime LastModifiedDate { get; set; }

    public Channel ModifyDescription(string newDescription) {
        Description = newDescription;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    public Channel ModifyRules(string newRules) {
        Rules = newRules;
        LastModifiedDate = DateTime.Now;
        return this;
    }

    public Channel ChangeIcon(string newIcon) {
        Icon = newIcon;
        LastModifiedDate = DateTime.Now;
        return this;
    }
}