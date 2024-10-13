using Chat.Server.Services;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Server.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly UserService _userService;
        private readonly MessageService _messageService;

        public ChatHub(UserService userService, MessageService messageService, ILogger<ChatHub> logger)
        {
            _userService = userService;
            _messageService = messageService;
            _logger = logger;
        }
        public async Task SendMessage(string message)
        {
            _logger.LogInformation("SendMessage called with message: {Message}", message);

            var user = await _userService.GetUserByConnectionIdAsync(Context.ConnectionId);
            if (user != null)
            {
                var msg = await _messageService.CreateMessageAsync(user, message);
                await Clients.All.SendAsync("ReceiveMessage", user.Username, message);
            }
        }

        public async Task SendPrivateMessage(string connectionId, string message)
        {
            var user = await _userService.GetUserByConnectionIdAsync(Context.ConnectionId);
            if (user != null)
            {
                var msg = await _messageService.CreateMessageAsync(user, message);
                await Clients.Client(connectionId).SendAsync("ReceivePrivateMessage", user.Username, message);
            }
        }

        public async Task SendGroupMessage(string group, string message)
        {
            var user = await _userService.GetUserByConnectionIdAsync(Context.ConnectionId);
            if (user != null)
            {
                var msg = await _messageService.CreateMessageAsync(user, message);
                await Clients.Group(group).SendAsync("ReceiveGroupMessage", user.Username, message);
            }
        }

        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task LeaveGroup(string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendDirectMessage(string connectionId, string message)
        {
            var user = await _userService.GetUserByConnectionIdAsync(Context.ConnectionId);
            if (user != null)
            {
                var msg = await _messageService.CreateMessageAsync(user, message);
                await Clients.Client(connectionId).SendAsync("ReceiveDirectMessage", user.Username, message);
            }
        }

        public async Task SendTyping()
        {
            var user = await _userService.GetUserByConnectionIdAsync(Context.ConnectionId);
            if (user != null)
            {
                await Clients.All.SendAsync("ReceiveTyping", user.Username);
            }
        }

        public async Task SendStopTyping()
        {
            var user = await _userService.GetUserByConnectionIdAsync(Context.ConnectionId);
            if (user != null)
            {
                await Clients.All.SendAsync("ReceiveStopTyping", user.Username);
            }
        }
    }
}
