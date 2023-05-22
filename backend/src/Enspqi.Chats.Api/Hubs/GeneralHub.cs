using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;

namespace Enspqi.Chats.Api.Hubs;

public class GeneralHub : Hub<IChatClient>
{
    private readonly ILogger<GeneralHub> _logger;
    private readonly IDistributedCache _cache;

    public GeneralHub(ILogger<GeneralHub> logger, IDistributedCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    public override Task OnConnectedAsync()
    {
        var id = Context.ConnectionId;
        var user = Context.User;
        
                        
        _logger.LogInformation("client connected");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("client disconnected");
        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendToAll(string msg)
    {
        await Clients.All.ReceiveGeneral(msg);
        //await Clients.All.SendAsync("messageReceived", msg);
    }
}
