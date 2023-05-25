using System.Collections.Concurrent;

namespace Enspqi.Chats.Api.Services.Storage;

public class MemStorageService : IStorageService
{
    private readonly ConcurrentDictionary<string, Room> _rooms;
    private readonly ConcurrentDictionary<string, ConnectedUser> _users;

    public MemStorageService(ConcurrentDictionary<string, Room> rooms,
        ConcurrentDictionary<string, ConnectedUser> users)
    {
        _rooms = rooms;
        _users = users;
    }

    public Task<ICollection<T>> GetAll<T>() where T : Storable
    {
        var collection = GetCollectionFor<T>();
        var all = ((ConcurrentDictionary<string, T>)collection).Values;

        return Task.FromResult(all);
    }

    public Task<T?> Get<T>(string Id) where T : Storable
    {
        var collection = (ConcurrentDictionary<string, T>)GetCollectionFor<T>();

        return Task.FromResult(collection.GetValueOrDefault(Id));
    }

    public Task Set<T>(T entry) where T : Storable
    {
        var collection = GetCollectionFor(entry);
        ((ConcurrentDictionary<string, T>)collection).AddOrUpdate(entry.Id, entry, (_, _) => entry);

        return Task.CompletedTask;
    }

    public Task Delete<T>(string id) where T : Storable
    {
        var collection = (ConcurrentDictionary<string, T>)GetCollectionFor<T>();
        collection.TryRemove(id, out var _);

        return Task.CompletedTask;
    }

    private object GetCollectionFor<T>(T entry) where T : Storable => entry switch
    {
        Room => _rooms,
        ConnectedUser => _users,
        _ => throw new NotSupportedException("this entry does not have a suitable memory storage")
    };

    private object GetCollectionFor<T>() where T : Storable => typeof(T).Name switch
    {
        nameof(Room) => _rooms,
        nameof(ConnectedUser) => _users,
        _ => throw new NotSupportedException("this entry does not have a suitable memory storage")
    };
}
