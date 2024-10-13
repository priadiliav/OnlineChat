namespace Chat.Server.Models;
public class User
{
    public long Id { get; set; }
    public string ConnectionId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
