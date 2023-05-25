using Enspqi.Chats.Api.Services;
using Microsoft.AspNetCore.SignalR;

namespace Enspqi.Chats.Api.Hubs;

public class GeneralHub : Hub<IChatClient>
{   
    private readonly IStorageService _storage;

    public GeneralHub(IStorageService storage)
    {        
        _storage = storage;
    }

    public override async Task OnConnectedAsync()
    {   
        var displayName = Context.GetHttpContext()?.Request.Query["displayName"].FirstOrDefault();

        if (displayName == null)
        {
            Context.Abort();
            throw new InvalidOperationException("display name not provided");
        }

        await _storage.Set(new ConnectedUser(Context.ConnectionId, displayName, Context.ConnectionId));
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await _storage.Delete<ConnectedUser>(Context.ConnectionId);
    }

    public async Task Join(string roomId)
    {
        var group = await _storage.Get<Room>(roomId) 
            ?? throw new InvalidOperationException("Group not exist");

        var user = await _storage.Get<ConnectedUser>(Context.ConnectionId);

        await Groups.AddToGroupAsync(Context.ConnectionId, group.Name);
        await Clients.OthersInGroup(group.Name).UserJoinedToRoom(user!.DisplayName, group.Id);
    }

    public async Task<Room> CreateAndJoin(string roomName)
    {        
        // TODO: check if the name already exists
        var newRoom = new Room(Guid.NewGuid().ToString(), roomName);
        
        await _storage.Set(newRoom);
        await Groups.AddToGroupAsync(Context.ConnectionId, newRoom.Name);
        await Clients.Others.NewRoomCreated(newRoom);
                
        return newRoom;
    }

    public async Task SendToAll(string msg)
    {
        // TODO: only for admins
        await Clients.All.ReceiveGeneral(msg);        
    }

    public async Task SendToRoom(string roomId, string msg)
    {
        var user = await _storage.Get<ConnectedUser>(Context.ConnectionId);
        var group = await _storage.Get<Room>(roomId) ?? throw new InvalidOperationException("Group not exist");

        await Clients.OthersInGroup(group.Name).ReceiveInRoom(roomId, user!.DisplayName, msg);
    }
}
