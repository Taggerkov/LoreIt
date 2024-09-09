using ServerEntities.Util;

namespace MemoryRepo.Util;

internal interface ICrud {
    static void Create(IEnumerable<IServerEntity> query, IServerEntity item) {
        var temp = query.ToList();
        if (item.Id != -1)
            throw new InvalidOperationException($"{item.EntityName} ({item.Id}) may have already been added! Expected ID: -1");
        item.Id = temp.Count > 0 ? temp.Max(u => u.Id) + 1 : 1;
        temp.Add(item);
    }

    static IServerEntity Read(IEnumerable<IServerEntity> query, int id, string entityName) {
        var item = query.FirstOrDefault(i => i.Id == id);
        if (item == null) throw new InvalidOperationException($"{entityName} ({id}) does not exist!");
        return item;
    }

    static void Update(IEnumerable<IServerEntity> query, IServerEntity item) {
        var temp = query.ToList();
        var oldItem = temp.FirstOrDefault(i => i.Id == item.Id);
        if (oldItem == null) throw new InvalidOperationException($"Post ({item.Id}) does not exist!");
        temp.Remove(oldItem);
        temp.Add(item);
    }

    static void Delete(IEnumerable<IServerEntity> query, int id, string entityName) {
        var temp = query.ToList();
        var oldItem = temp.FirstOrDefault(i => i.Id == id);
        if (oldItem == null) throw new InvalidOperationException($"{entityName} ({id}) does not exist!");
        temp.Remove(oldItem);
    }
}