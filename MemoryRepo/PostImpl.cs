using MemoryRepo.Util;
using RepoContracts;
using ServerEntities;
using ServerEntities.Util;

namespace MemoryRepo;

public class PostImpl : ICrud, IPostRepo {
    private List<Post> Posts { get; set; } = new();
    private const string EntityType = "Post";

    public IQueryable<Post> GetAllPosts() {
        return Posts.AsQueryable();
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

    private List<IServerEntity> GetGenericList() {
        var genericList = Posts.Cast<IServerEntity>().ToList();
        Posts = genericList.Cast<Post>().ToList();
        return genericList;
    }
}