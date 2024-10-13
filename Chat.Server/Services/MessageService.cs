using Chat.Server.Contracts;
using Chat.Server.Models;

namespace Chat.Server.Services;
public class MessageService
{
    private IMessageContract _messageContract;
    public MessageService(IMessageContract messageContract)
    {
        _messageContract = messageContract;
    }
    public async Task<Message> CreateMessageAsync(User sender, string content)
    {
        return await _messageContract.CreateMessageAsync(sender, content);
    }
    public async Task<Message?> GetMessageAsync(long id)
    {
        return await _messageContract.GetMessageAsync(id);
    }
    public async Task<IEnumerable<Message>> GetMessagesAsync()
    {
        return await _messageContract.GetMessagesAsync();
    }
    public async Task<Message> UpdateMessageAsync(Message message)
    {
        return await _messageContract.UpdateMessageAsync(message);
    }
    public async Task DeleteMessageAsync(long id)
    {
        await _messageContract.DeleteMessageAsync(id);
    }
}
