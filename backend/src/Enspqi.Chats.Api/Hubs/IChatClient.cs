namespace Enspqi.Chats.Api.Hubs;

public interface IChatClient
{    
    Task ReceiveGeneral(string msg);
    Task ReceiveInRoom(string roomId, string userName, string msg);
    
    Task NewRoomCreated(Room room);
    Task UserJoinedToRoom(string userName, string roomId);
}
