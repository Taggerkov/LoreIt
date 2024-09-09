using ServerEntities;

namespace RepoContracts;

public interface IPostRepo {
    IQueryable<Post> GetAllPosts();
    IQueryable<Post> GetAllWithoutChannel();
    IQueryable<Post> GetAllFromChannel(int channelId);
    Task<Post> GetAsync(int id);
    Task<Post> AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(int id);
}