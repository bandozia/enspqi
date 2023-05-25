using System.Collections.Concurrent;

namespace Enspqi.Chats.Api.Services;

public interface IStorageService
{    
    Task<ICollection<T>> GetAll<T>() where T : Storable;
    Task<T?> Get<T>(string Id) where T : Storable;
    Task Set<T>(T entry) where T : Storable;        
    Task Delete<T>(string id) where T : Storable;
}
