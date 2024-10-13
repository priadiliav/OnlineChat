using Chat.Server.Contracts;
using Chat.Server.Models;
using StackExchange.Redis;

namespace Chat.Server.Repositories.Redis;
public class RedisUserRepository : IUserContract
{
    private readonly IConnectionMultiplexer _redis;

    public RedisUserRepository(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<User> CreateUserAsync(string connectionId, string username)
    {
        var db = _redis.GetDatabase();

        var userId = await db.StringIncrementAsync("id_increment:id");

        var user = new User
        {
            Id = userId,
            ConnectionId = connectionId,
            Username = username,
            CreatedAt = DateTime.UtcNow
        };

        var userKey = $"user:{user.Id}";
        await db.HashSetAsync(userKey, new HashEntry[]
        {
            new HashEntry("Id", user.Id),
            new HashEntry("ConnectionId", user.ConnectionId),
            new HashEntry("Username", user.Username),
            new HashEntry("CreatedAt", user.CreatedAt.ToString("o"))
        });

        return user;
    }

    public async Task DeleteUserAsync(long id)
    {
        var db = _redis.GetDatabase();
        var userKey = $"user:{id}";
        await db.KeyDeleteAsync(userKey);
    }

    public async Task<User?> GetUserAsync(long id)
    {
        var db = _redis.GetDatabase();
        var userHash = await db.HashGetAllAsync($"user:{id}");
        if (userHash.Length == 0)
        {
            return null;
        }

        return new User
        {
            Id = (long)userHash.FirstOrDefault(x => x.Name == "Id").Value,
            ConnectionId = userHash.FirstOrDefault(x => x.Name == "ConnectionId").Value,
            Username = userHash.FirstOrDefault(x => x.Name == "Username").Value,
            CreatedAt = DateTime.Parse(userHash.FirstOrDefault(x => x.Name == "CreatedAt").Value)
        };
    }

    public async Task<User?> GetUserByConnectionIdAsync(string connectionId)
    {
        var db = _redis.GetDatabase();
        var server = _redis.GetServer(_redis.GetEndPoints().First());
        var keys = server.Keys(pattern: "user:*");

        foreach (var key in keys)
        {
            var userHash = await db.HashGetAllAsync(key);
            if (userHash.FirstOrDefault(x => x.Name == "ConnectionId").Value == connectionId)
            {
                return new User
                {
                    Id = (long)userHash.FirstOrDefault(x => x.Name == "Id").Value,
                    ConnectionId = userHash.FirstOrDefault(x => x.Name == "ConnectionId").Value,
                    Username = userHash.FirstOrDefault(x => x.Name == "Username").Value,
                    CreatedAt = DateTime.Parse(userHash.FirstOrDefault(x => x.Name == "CreatedAt").Value)
                };
            }
        }

        return null;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var db = _redis.GetDatabase();
        var server = _redis.GetServer(_redis.GetEndPoints().First());
        var keys = server.Keys(pattern: "user:*");

        foreach (var key in keys)
        {
            var userHash = await db.HashGetAllAsync(key);
            if (userHash.FirstOrDefault(x => x.Name == "Username").Value == username)
            {
                return new User
                {
                    Id = (long)userHash.FirstOrDefault(x => x.Name == "Id").Value,
                    ConnectionId = userHash.FirstOrDefault(x => x.Name == "ConnectionId").Value,
                    Username = userHash.FirstOrDefault(x => x.Name == "Username").Value,
                    CreatedAt = DateTime.Parse(userHash.FirstOrDefault(x => x.Name == "CreatedAt").Value)
                };
            }
        }

        return null;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        var db = _redis.GetDatabase();
        var server = _redis.GetServer(_redis.GetEndPoints().First());
        var keys = server.Keys(pattern: "user:*");

        var users = new List<User>();
        foreach (var key in keys)
        {
            var userHash = await db.HashGetAllAsync(key);
            users.Add(new User
            {
                Id = (long)userHash.FirstOrDefault(x => x.Name == "Id").Value,
                ConnectionId = userHash.FirstOrDefault(x => x.Name == "ConnectionId").Value,
                Username = userHash.FirstOrDefault(x => x.Name == "Username").Value,
                CreatedAt = DateTime.Parse(userHash.FirstOrDefault(x => x.Name == "CreatedAt").Value)
            });
        }

        return users;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        var db = _redis.GetDatabase();
        var userKey = $"user:{user.Id}";
        await db.HashSetAsync(userKey, new HashEntry[]
        {
            new HashEntry("ConnectionId", user.ConnectionId),
            new HashEntry("Username", user.Username),
            new HashEntry("CreatedAt", user.CreatedAt.ToString("o"))
        });

        return user;
    }
}
