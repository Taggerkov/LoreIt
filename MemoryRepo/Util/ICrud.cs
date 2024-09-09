using ServerEntities.Util;

namespace MemoryRepo.Util;

internal interface ICrud {
    
    #pragma warning disable CA1860
    static void Create(List<IServerEntity> query, IServerEntity item) {
        if (item.Id != -1) throw new InvalidOperationException($"{item.EntityName} ({item.Id}) may have already been added! Expected ID: -1");
        item.Id = query.Any() ? query.Max( u => u.Id) + 1 : 1;
        query.Add(item);
    }

    static IServerEntity Read(List<IServerEntity> query, int id, string entityName) {
        var item = query.FirstOrDefault(i => i.Id == id);
        if (item == null) throw new InvalidOperationException($"{entityName} ({id}) does not exist!");
        return item;
    }
    
    static void Update(List<IServerEntity> query, IServerEntity item) {
        var oldItem = query.FirstOrDefault(i => i.Id == item.Id);
        if (oldItem == null) throw new InvalidOperationException($"Post ({item.Id}) does not exist!");
        query.Remove(oldItem);
        query.Add(item);
    }

    static void Delete(List<IServerEntity> query, int id, string entityName) {
        var oldItem = query.FirstOrDefault(i => i.Id == id);
        if (oldItem == null) throw new InvalidOperationException($"{entityName} ({id}) does not exist!");
        query.Remove(oldItem);
    }
}