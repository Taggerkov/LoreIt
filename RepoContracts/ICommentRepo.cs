using ServerEntities;

namespace RepoContracts;

public interface ICommentRepo {
    IQueryable<Comment> GetAllComments();
    IQueryable<Comment> GetAllFromPost(int postId);
    Task<Comment> GetAsync(int id);
    Task<Comment> AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int id);
}