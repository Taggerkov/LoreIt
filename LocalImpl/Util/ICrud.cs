using ServerEntities.Util;

namespace LocalImpl.Util;

/// <summary>
/// Defines common CRUD operations for entities implementing the IServerEntity interface.
/// </summary>
internal interface ICrud {
    /// Adds a new IServerEntity to the collection and assigns it a unique ID.
    /// <param name="query">The collection of existing entities.</param>
    /// <param name="item">The entity to be added to the collection.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated list of IServerEntity objects including the newly added entity.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the entity being added already has a non-default ID, suggesting it may have already been added.</exception>
    static async Task<List<IServerEntity>> CreateAsync(IEnumerable<IServerEntity> query, IServerEntity item) {
        var temp = query.ToList();
        if (item.Id != -1)
            throw new InvalidOperationException($"{item.EntityName} ({item.Id}) may have already been added! Expected ID: -1");
        item.Id = temp.Count > 0 ? temp.Max(u => u.Id) + 1 : 1;
        temp.Add(item);
        return await Task.FromResult(temp);
    }

    /// Retrieves an IServerEntity from the collection based on the specified ID.
    /// <param name="query">The collection of existing entities.</param>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <param name="entityName">The name of the entity to help identify it in error messages.</param>
    /// <returns>The IServerEntity matching the specified ID.</returns>
    /// <exception cref="InvalidOperationException">Thrown when an entity with the specified ID does not exist in the collection.</exception>
    static async Task<IServerEntity> ReadAsync(IEnumerable<IServerEntity> query, int id, string entityName) {
        var item = query.FirstOrDefault(i => i.Id == id);
        if (item == null) throw new InvalidOperationException($"{entityName} ({id}) does not exist!");
        return await Task.FromResult(item);
    }

    /// Updates an existing IServerEntity item in a given list of entities.
    /// <param name="query">The collection of existing entities.</param>
    /// <param name="item">The entity with updated information to replace the existing one.</param>
    /// <returns>A list containing the entities with the updated entity included.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the entity with the specified ID does not exist in the collection.</exception>
    static async Task<List<IServerEntity>> UpdateAsync(IEnumerable<IServerEntity> query, IServerEntity item) {
        var temp = query.ToList();
        var oldItem = temp.FirstOrDefault(i => i.Id == item.Id);
        if (oldItem == null) throw new InvalidOperationException($"Post ({item.Id}) does not exist!");
        temp.Remove(oldItem);
        temp.Add(item);
        return await Task.FromResult(temp);
    }

    /// Removes an IServerEntity item from a given list of entities by its ID.
    /// <param name="query">The collection of existing entities.</param>
    /// <param name="id">The ID of the entity to be removed.</param>
    /// <param name="entityName">The name of the entity to be removed.</param>
    /// <returns>A list containing the entities excluding the removed entity.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the entity with the specified ID does not exist.</exception>
    static async Task<List<IServerEntity>> DeleteAsync(IEnumerable<IServerEntity> query, int id, string entityName) {
        var temp = query.ToList();
        var oldItem = temp.FirstOrDefault(i => i.Id == id);
        if (oldItem == null) throw new InvalidOperationException($"{entityName} ({id}) does not exist!");
        temp.Remove(oldItem);
        return await Task.FromResult(temp);
    }
}