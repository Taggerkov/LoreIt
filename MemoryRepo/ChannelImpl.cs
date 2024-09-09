using MemoryRepo.Util;
using RepoContracts;
using ServerEntities;
using ServerEntities.Util;

namespace MemoryRepo;

public class ChannelImpl : ICrud, IChannelRepo {
    private List<Channel> Channels { get; set; } = new();
    private const string EntityType = "Channel";

    public IQueryable<Channel> GetAllChannels() {
        return Channels.AsQueryable();
    }

    public Task<Channel> GetAsync(int id) {
        if (ICrud.Read(GetGenericList(), id, EntityType) is Channel channel) return Task.FromResult(channel);
        throw new Exception("Couldn't read Channel or possible data corruption.");
    }

    public Task<Channel> AddAsync(Channel channel) {
        ICrud.Create(GetGenericList(), channel);
        return Task.FromResult(channel);
    }

    public Task UpdateAsync(Channel channel) {
        ICrud.Update(GetGenericList(), channel);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id) {
        ICrud.Delete(GetGenericList(), id, EntityType);
        return Task.CompletedTask;
    }

    private List<IServerEntity> GetGenericList() {
        var genericList = Channels.Cast<IServerEntity>().ToList();
        Channels = genericList.Cast<Channel>().ToList();
        return genericList;
    }
}