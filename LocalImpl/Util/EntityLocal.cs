using System.Text.Json;
using ServerEntities.Util;

namespace LocalImpl.Util;

/// <summary>
/// Provides an abstract base class for handling local operations related to entities that implement the IServerEntity interface.
/// </summary>
/// <typeparam name="T">The type of entity that the class will operate on. The type must implement the IServerEntity interface.</typeparam>
public abstract class EntityLocal<T> where T : IServerEntity {
    /// <summary>
    /// A private field that holds the type of the entity as a string.
    /// This field is utilized internally to specify and distinguish the entity type
    /// within various operations of the <see cref="EntityLocal{T}"/> class.
    /// </summary>
    private string _entityType = string.Empty;

    /// <summary>
    /// Sets the entity type for the current instance.
    /// </summary>
    /// <param name="entityType">The type of the entity to set.</param>
    protected void Build(string entityType) {
        _entityType = entityType;
    }

    /// <summary>
    /// Retrieves all entities in the provided list as an IQueryable.
    /// </summary>
    /// <param name="list">The list of entities to retrieve.</param>
    /// <returns>An IQueryable representing all entities in the list.</returns>
    protected IQueryable<T> _GetAll(List<T> list) {
        return list.AsQueryable();
    }

    /// <summary>
    /// Retrieves an entity from the provided list asynchronously based on the given ID.
    /// </summary>
    /// <param name="list">The list of entities from which an entity will be retrieved.</param>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>A task representing the asynchronous operation, containing the retrieved entity.</returns>
    /// <exception cref="Exception">Thrown when the entity could not be read or in the case of possible data corruption.</exception>
    protected async Task<T> _GetAsync(List<T> list, int id) {
        if (await ICrud.ReadAsync(_GetGenericList(list), id, _entityType) is not T entity) throw new Exception($"Couldn't read {_entityType} or possible data corruption.");
        return entity;
    }

    /// <summary>
    /// Adds a new entity to the provided list asynchronously.
    /// </summary>
    /// <param name="list">The list to which the new entity will be added.</param>
    /// <param name="entity">The entity to add to the list.</param>
    /// <returns>A task representing the asynchronous operation, containing the updated list with the new entity added.</returns>
    protected async Task<List<T>> _AddAsync(List<T> list, T entity) {
        return (await ICrud.CreateAsync(_GetGenericList(list), entity)).Cast<T>().ToList();
    }

    /// <summary>
    /// Updates an existing entity in the provided list asynchronously.
    /// </summary>
    /// <param name="list">The list of entities to be updated.</param>
    /// <param name="entity">The entity with updated information.</param>
    /// <returns>A task representing the asynchronous operation, containing the list with the updated entity.</returns>
    protected async Task<List<T>> _UpdateAsync(List<T> list, T entity) {
        return (await ICrud.UpdateAsync(_GetGenericList(list), entity)).Cast<T>().ToList();
    }

    /// <summary>
    /// Deletes an entity from the provided list asynchronously based on the specified ID.
    /// </summary>
    /// <param name="list">The list of entities from which the entity should be deleted.</param>
    /// <param name="id">The ID of the entity to be deleted.</param>
    /// <returns>A task representing the asynchronous operation, containing a list of entities with the specified entity removed.</returns>
    protected async Task<List<T>> _DeleteAsync(List<T> list, int id) {
        return (await ICrud.DeleteAsync(_GetGenericList(list), id, _entityType)).Cast<T>().ToList();
    }

    /// <summary>
    /// Converts a list of entities of type T to a list of IServerEntity.
    /// </summary>
    /// <param name="list">The list of entities to be converted.</param>
    /// <returns>A list of entities cast to IServerEntity.</returns>
    private static IEnumerable<IServerEntity> _GetGenericList(List<T> list) {
        return list.Cast<IServerEntity>();
    }

    /// <summary>
    /// Asynchronously saves a list of entities to a specified local path.
    /// </summary>
    /// <param name="list">The list of entities to be saved.</param>
    /// <param name="localPath">The path to the local file where the entities will be saved.</param>
    /// <exception cref="IOException">Thrown when the data could not be saved to the file.</exception>
    protected async Task _Save(List<T> list, string localPath) {
        try {
            await File.WriteAllTextAsync(localPath, JsonSerializer.Serialize(list));
        }
        catch (Exception) {
            throw new IOException("Couldn't save changes!");
        }
    }
}