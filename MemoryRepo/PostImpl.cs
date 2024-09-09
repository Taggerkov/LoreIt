using MemoryRepo.Util;
using RepoContracts;
using ServerEntities;
using ServerEntities.Util;

namespace MemoryRepo;

public class PostImpl : ICrud, IPostRepo {
    private List<Post> Posts { get; set; } = [];
    private const string EntityType = "Post";

    public IQueryable<Post> GetAllPosts() {
        return Posts.AsQueryable();
    }

    public IQueryable<Post> GetAllWithoutChannel() {
        return Posts.Where(p => p.ChannelId == -1).AsQueryable();
    }

    public IQueryable<Post> GetAllFromChannel(int channelId) {
        return Posts.Where(p => p.ChannelId == channelId).AsQueryable();
    }

    public Task<Post> GetAsync(int id) {
        if (ICrud.Read(GetGenericList(), id, EntityType) is Post post) return Task.FromResult(post);
        throw new Exception("Couldn't read Post or possible data corruption.");
    }

    public Task<Post> AddAsync(Post post) {
        ICrud.Create(GetGenericList(), post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post) {
        ICrud.Update(GetGenericList(), post);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id) {
        ICrud.Delete(GetGenericList(), id, EntityType);
        return Task.CompletedTask;
    }

    #pragma warning disable CA1859
    private IEnumerable<IServerEntity> GetGenericList() {
        return Posts;
    }
}