namespace Enspqi.Chats.Api.Hubs;

public interface IChatClient
{    
    Task ReceiveGeneral(string msg);
}
