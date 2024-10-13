using Chat.Server.Models;
using Chat.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Server.Controllers;

[ApiController]
[Route("api/messages")]
public class MessageController (MessageService messageService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Message>> GetMessagesAsync()
    {
        return await messageService.GetMessagesAsync();
    }

    [HttpGet("{id}")]
    public async Task<Message?> GetMessageAsync(long id)
    {
        return await messageService.GetMessageAsync(id);
    }

    [HttpPost]
    public async Task<Message> CreateMessageAsync([FromBody] Message message)
    {
        return await messageService.CreateMessageAsync(message.Sender, message.Content);
    }

    [HttpPut]
    public async Task<Message> UpdateMessageAsync([FromBody] Message message)
    {
        return await messageService.UpdateMessageAsync(message);
    }

    [HttpDelete("{id}")]
    public async Task DeleteMessageAsync(long id)
    {
        await messageService.DeleteMessageAsync(id);
    }
}

