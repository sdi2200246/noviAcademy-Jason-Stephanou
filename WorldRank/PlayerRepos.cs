namespace WorldRank;

public interface IPlayerRepo
{
    public void AddPlayer(IPlayer player);
    public void DeletePlayer(Guid playerId);
    public IPlayer FindPlayer(Guid playerId);
    public List<IPlayer> GroupPlayersByScore();

    public void ListPlayers();
}

public class PlayerRepo : IPlayerRepo
{
    Dictionary<Guid, IPlayer> Players;

    public PlayerRepo(Dictionary<Guid, IPlayer> registry)
    {
        if (registry is null)
        {
            throw new ArgumentNullException("Repository cannot be null");
        }
        Players = registry;
    }

    public void AddPlayer(IPlayer player)
    {
        if (player == null)
            throw new ArgumentNullException(nameof(player));
        if (Players.ContainsKey(player.Id))
            throw new InvalidOperationException("Player with the same ID already exists.");
        Players[player.Id] = player;
    }
    public void DeletePlayer(Guid playerId)
    {
        if (!Players.Remove(playerId))
            throw new KeyNotFoundException("Player not found.");
    }
    public IPlayer FindPlayer(Guid playerId)
    {
        if (Players.TryGetValue(playerId, out var player))
            return player;
        throw new KeyNotFoundException("Player not found.");
    }
    public List<IPlayer> GroupPlayersByScore()
    {
        return Players.Values.OrderByDescending(p => p.Score).ToList();
    }
    public void ListPlayers()
    {
        if (Players.Count == 0)
        {
            Console.WriteLine("No players.");
            return;
        }

        foreach (var player in Players.Values)
            Console.WriteLine(player.ToString());
    }
}


