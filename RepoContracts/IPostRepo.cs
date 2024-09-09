using ServerEntities;

namespace RepoContracts;

public interface IPostRepo {
    IQueryable<Post> GetAllPosts();
    Task<Post> GetAsync(int id);
    Task<Post> AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(int id);
}