using Enspqi.Chats.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace Enspqi.Chats.Api.Handlers;

public class RoomHandler
{    
    public static async Task<IResult> GetAllRooms(IStorageService storage)
    {                
        return Ok(await storage.GetAll<Room>());
    }
}
