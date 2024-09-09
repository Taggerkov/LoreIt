using MemoryRepo.Util;
using RepoContracts;
using ServerEntities;
using ServerEntities.Util;

namespace MemoryRepo;

public class CommentImpl : ICrud, ICommentRepo {
    private List<Comment> Comments { get; set; } = new();
    private const string EntityType = "Comment";

    public IQueryable<Comment> GetAllComments() {
        return Comments.AsQueryable();
    }

    public IQueryable<Comment> GetAllFromPost(int postId) {
        try {
            return Comments.Where(c => c.PostId == postId).AsQueryable();
        }
        catch (ArgumentNullException) {
            throw new InvalidOperationException($"Post ({postId}) does not exist or has no comments!");
        }
    }

    public Task<Comment> GetAsync(int id) {
        if (ICrud.Read(GetGenericList(), id, EntityType) is Comment comment) return Task.FromResult(comment);
        throw new Exception("Couldn't read Comment or possible data corruption.");
    }

    public Task<Comment> AddAsync(Comment comment) {
        ICrud.Create(GetGenericList(), comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment) {
        ICrud.Update(GetGenericList(), comment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id) {
        ICrud.Delete(GetGenericList(), id, EntityType);
        return Task.CompletedTask;
    }

    private List<IServerEntity> GetGenericList() {
        var genericList = Comments.Cast<IServerEntity>().ToList();
        Comments = genericList.Cast<Comment>().ToList();
        return genericList;
    }
}