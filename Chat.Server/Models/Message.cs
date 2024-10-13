namespace Chat.Server.Models;
public class Message
{
    public long Id { get; set; }
    public User? Sender { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
