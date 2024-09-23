using RepoContracts.Util;
using ServerEntities;

namespace RepoContracts;

/// Provides methods for managing comments in the repository.
public interface ICommentRepo : IServerEntityRepo<Comment> {
    /// Retrieves all comments associated with a specific post.
    /// <param name="postId">The unique identifier of the post whose comments are to be retrieved.</param>
    /// <return>An IQueryable collection of comments associated with the specified post.</return>
    IQueryable<Comment> GetAllFromPost(int postId);
}