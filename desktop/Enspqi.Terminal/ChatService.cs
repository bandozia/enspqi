using Microsoft.AspNetCore.SignalR.Client;

namespace Enspqi.Terminal;

public class ChatService : IChatService
{
    private HubConnection? _connection;

    public event ConnectionDownDelegate? OnConnectionDown;
    public event NewRoomCreatedDelegate? OnNewRoomCreated;

    public Task Connect(string url)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl(url)
            .Build();

        _connection.Closed += _connection_Closed;

        _connection.On<Room>("NewRoomCreated", r => OnNewRoomCreated?.Invoke(r));
                
        return _connection.StartAsync();
    }

    public Task<Room> CreateRoomAndJoin(string roomName) =>
        _connection!.InvokeAsync<Room>("CreateAndJoin", roomName);

    private Task _connection_Closed(Exception? arg)
    {
        OnConnectionDown?.Invoke();
        return Task.CompletedTask;
    }
}
