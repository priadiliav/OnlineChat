using Chat.Server.Models;
using Chat.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Server.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(UserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await userService.GetUsersAsync();
    }

    [HttpGet("{id}")]
    public async Task<User?> GetUserAsync(long id)
    {
        return await userService.GetUserAsync(id);
    }

    [HttpGet("connectionId/{connectionId}")]
    public async Task<User?> GetUserByConnectionIdAsync(string connectionId)
    {
        return await userService.GetUserByConnectionIdAsync(connectionId);
    }

    [HttpGet("username/{username}")]
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await userService.GetUserByUsernameAsync(username);
    }

    [HttpPost]
    public async Task<User> CreateUserAsync([FromBody] User user)
    {
        return await userService.CreateUserAsync(user.ConnectionId, user.Username);
    }

    [HttpPut]
    public async Task<User> UpdateUserAsync([FromBody] User user)
    {
        return await userService.UpdateUserAsync(user);
    }

    [HttpDelete("{id}")]
    public async Task DeleteUserAsync(long id)
    {
        await userService.DeleteUserAsync(id);
    }
}
