using Chat.Server.Contracts;
using Chat.Server.Models;
using StackExchange.Redis;

namespace Chat.Server.Repositories.Redis;

public class RedisMessageRepository : IMessageContract
{
    private readonly IConnectionMultiplexer _redis;

    public RedisMessageRepository(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public Task<Message> CreateMessageAsync(User sender, string content)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMessageAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Message> GetMessageAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Message>> GetMessagesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Message> UpdateMessageAsync(Message message)
    {
        throw new NotImplementedException();
    }
}
