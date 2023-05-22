using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Distributed;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Enspqi.Chats.Api.Handlers;

public class RoomHandler
{
    public static async Task<IResult> GetAllRooms()
    {        
        return Ok("");
    }

    public static async Task<Created<Room>> CreateRoom(Room room, IDistributedCache cache)
    {
        
        return Created("/", room);
    }
}
