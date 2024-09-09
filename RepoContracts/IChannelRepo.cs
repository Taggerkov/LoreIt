using ServerEntities;

namespace RepoContracts;

public interface IChannelRepo {
    IQueryable<Channel> GetAllChannels();
    Task<Channel> GetAsync(int id);
    Task<Channel> AddAsync(Channel channel);
    Task UpdateAsync(Channel channel);
    Task DeleteAsync(int id);
}