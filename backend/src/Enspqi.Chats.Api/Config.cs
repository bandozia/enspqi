using System.Collections.Concurrent;
using Enspqi.Chats.Api.Services;
using Enspqi.Chats.Api.Services.Storage;

namespace Enspqi.Chats.Api;

public static class Config
{
    public static IServiceCollection AddMemoryStorage(this IServiceCollection services)
    {   
        var rooms = new ConcurrentDictionary<string, Room>();
        var users = new ConcurrentDictionary<string, ConnectedUser>();

        services.AddSingleton(f => rooms);
        services.AddSingleton(f => users);

        services.AddSingleton<IStorageService, MemStorageService>();

        return services;
    }
}
