namespace APILibrary;

public class CommentDto {
    public int Id { get; set; }
    public int CommenterId { get; set; }
    public int PostId { get; set; }
    public string Content { get; set; }
}