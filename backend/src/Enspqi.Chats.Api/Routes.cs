using static Enspqi.Chats.Api.Handlers.RoomHandler;

namespace Enspqi.Chats.Api;

public static class Routes
{
    public static RouteGroupBuilder MapGeneralRooms(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetAllRooms);
        
        return builder;
    }
}
