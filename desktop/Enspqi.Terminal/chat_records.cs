namespace Enspqi.Terminal;

public record Storable(string Id);

public record Room(string Id, string Name) : Storable(Id);

public record ConnectedUser(string Id, string DisplayName, string ConnectionId) : Storable(Id);