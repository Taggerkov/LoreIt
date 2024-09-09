namespace ServerEntities.Util;

public interface IServerEntity {
    int Id { get; set; }
    string EntityName { get; }
    DateTime PublishDate { get; set; }
    DateTime LastModifiedDate { get; set; }
}