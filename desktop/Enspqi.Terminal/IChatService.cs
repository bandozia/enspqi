namespace Enspqi.Terminal;

public delegate void ConnectionDownDelegate();
public delegate void OnNewRoomCreatedDelegate();
public delegate void NewRoomCreatedDelegate(Room room);

public interface IChatService
{
    event ConnectionDownDelegate? OnConnectionDown;
    event NewRoomCreatedDelegate? OnNewRoomCreated;

    Task Connect(string url);

    Task<Room> CreateRoomAndJoin(string roomName);
}
