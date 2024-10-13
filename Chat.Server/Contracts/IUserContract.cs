using Chat.Server.Models;

namespace Chat.Server.Contracts;
public interface IUserContract
{
    Task<User?> GetUserAsync(long id);
    Task<User?> GetUserByConnectionIdAsync(string connectionId);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> CreateUserAsync(string connectionId, string username);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(long id);
}
