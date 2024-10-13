using Chat.Server.Models;

namespace Chat.Server.Contracts;
public interface IMessageContract
{
    Task<Message> GetMessageAsync(long id);
    Task<IEnumerable<Message>> GetMessagesAsync();
    Task<Message> CreateMessageAsync(User sender, string content);
    Task<Message> UpdateMessageAsync(Message message);
    Task DeleteMessageAsync(long id);
}
