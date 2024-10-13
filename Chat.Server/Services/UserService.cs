using Chat.Server.Contracts;
using Chat.Server.Models;

namespace Chat.Server.Services;
public class UserService
{
    private IUserContract _userContract;

    public UserService(IUserContract userContract)
    {
        _userContract = userContract;
    }
    public async Task<User?> GetUserByConnectionIdAsync(string connectionId)
    {
        return await _userContract.GetUserByConnectionIdAsync(connectionId);
    }
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _userContract.GetUserByUsernameAsync(username);
    }
    public async Task<User> CreateUserAsync(string connectionId, string username)
    {
        return await _userContract.CreateUserAsync(connectionId, username);
    }
    public async Task<User?> GetUserAsync(long id)
    {
        return await _userContract.GetUserAsync(id);
    }
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _userContract.GetUsersAsync();
    }
    public async Task<User> UpdateUserAsync(User user)
    {
        return await _userContract.UpdateUserAsync(user);
    }
    public async Task DeleteUserAsync(long id)
    {
        await _userContract.DeleteUserAsync(id);
    }
}