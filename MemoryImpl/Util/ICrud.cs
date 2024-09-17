using ServerEntities.Util;

namespace MemoryRepo.Util;

/// ICrud interface provides basic CRUD operations for managing collections of IServerEntity objects.
internal interface ICrud {
    /// Adds a new IServerEntity item to a given list of entities.
    /// <param name="query">The collection of existing entities.</param>
    /// <param name="item">The new entity to add to the collection.</param>
    /// <returns>A list containing the original entities along with the newly added entity.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the item's ID is not -1, indicating it has already been added.</exception>
    static List<IServerEntity> Create(IEnumerable<IServerEntity> query, IServerEntity item) {
        var temp = query.ToList();
        if (item.Id != -1)
            throw new InvalidOperationException($"{item.EntityName} ({item.Id}) may have already been added! Expected ID: -1");
        item.Id = temp.Count > 0 ? temp.Max(u => u.Id) + 1 : 1;
        temp.Add(item);
        return temp;
    }

    /// Retrieves an IServerEntity item from a given list of entities based on the specified ID.
    /// <param name="query">The collection of existing entities.</param>
    /// <param name="id">The unique identifier of the entity to be retrieved.</param>
    /// <param name="entityName">The name of the entity type for which the item is being retrieved.</param>
    /// <returns>The IServerEntity item that matches the specified ID.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the item with the specified ID does not exist in the collection.</exception>
    static IServerEntity Read(IEnumerable<IServerEntity> query, int id, string entityName) {
        var item = query.FirstOrDefault(i => i.Id == id);
        if (item == null) throw new InvalidOperationException($"{entityName} ({id}) does not exist!");
        return item;
    }

    /// Updates an existing IServerEntity item in a given list of entities.
    /// <param name="query">The collection of existing entities.</param>
    /// <param name="item">The entity with updated information.</param>
    /// <returns>A list containing the entities with the specified entity updated.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the item to update does not exist in the collection.</exception>
    static List<IServerEntity> Update(IEnumerable<IServerEntity> query, IServerEntity item) {
        var temp = query.ToList();
        var oldItem = temp.FirstOrDefault(i => i.Id == item.Id);
        if (oldItem == null) throw new InvalidOperationException($"Post ({item.Id}) does not exist!");
        temp.Remove(oldItem);
        temp.Add(item);
        return temp;
    }

    /// Deletes an IServerEntity item from a given list of entities.
    /// <param name="query">The collection of existing entities.</param>
    /// <param name="id">The ID of the entity to be deleted.</param>
    /// <param name="entityName">The name of the entity type.</param>
    /// <returns>A list of entities with the specified entity removed.</returns>
    /// <exception cref="InvalidOperationException">Thrown when an entity with the specified ID does not exist in the collection.</exception>
    static List<IServerEntity> Delete(IEnumerable<IServerEntity> query, int id, string entityName) {
        var temp = query.ToList();
        var oldItem = temp.FirstOrDefault(i => i.Id == id);
        if (oldItem == null) throw new InvalidOperationException($"{entityName} ({id}) does not exist!");
        temp.Remove(oldItem);
        return temp;
    }
}