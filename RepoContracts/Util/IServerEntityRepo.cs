using ServerEntities.Util;

namespace RepoContracts.Util;

/// Interface representing a repository for managing server entities.
public interface IServerEntityRepo<T> where T : IServerEntity {
    /// <summary>Retrieves all entities from the repository.</summary>
    /// <returns>An IQueryable collection of all entities of type T in the repository.</returns>
    IQueryable<T> GetAll();

    /// <summary>Retrieves an entity asynchronously by its identifier.</summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>A task representing the asynchronous operation, with a result containing the retrieved entity.</returns>
    Task<T> GetAsync(int id);

    /// Asynchronously adds a new entity to the repository.
    /// <param name="serverEntity">The entity to be added to the repository.</param>
    /// <returns>A Task representing the asynchronous operation, with the added entity as the result.</returns>
    Task<T> AddAsync(T serverEntity);

    /// <summary>Asynchronously updates an existing entity in the repository.</summary>
    /// <param name="serverEntity">The entity object with updated information.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(T serverEntity);

    /// <summary>Deletes an entity asynchronously based on its unique identifier.</summary>
    /// <param name="id">The unique identifier of the entity to be deleted.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task DeleteAsync(int id);
}