namespace WorldRank;

public interface IPlayer
{
    Dictionary<Currency , Iwallet> Wallets {get; }
    Guid Id { get; }
    string Name { get; }
    int Score { get; }
    void UpdateScore(int newScore);
}


public class Player: IPlayer
{

    public Dictionary<Currency, Iwallet> Wallets { get; private set;} = new Dictionary<Currency, Iwallet>();
    public Guid Id { get; }
    public string Name { get; }
    public int Score { get; private set; }

    public Player(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        Id = Guid.NewGuid();
        Name = name;
    }

    public void UpdateScore(int newScore)
    {
        if (newScore < 0)
            throw new ArgumentOutOfRangeException(nameof(newScore), "Score cannot be negative.");

        Score = newScore;
    }

    public override string ToString() =>
            $"[{Id}] {Name} - Score: {Score}";
}

public interface IPlayerRepo
{
    public void AddPlayer(IPlayer player);
    public void DeletePlayer(Guid playerId);
    public IPlayer FindPlayer(Guid playerId);
    public List<IPlayer> GroupPlayersByScore();
}


public class PlayerRepo : IPlayerRepo
{
    public Dictionary<Guid, IPlayer> _players { get; private set; } = new Dictionary<Guid, IPlayer>();
    public void AddPlayer(IPlayer player)
    {
        if (player == null)
            throw new ArgumentNullException(nameof(player));
        if (_players.ContainsKey(player.Id))
            throw new InvalidOperationException("Player with the same ID already exists.");
        _players[player.Id] = player;
    }
    public void DeletePlayer(Guid playerId)
    {
        if (!_players.Remove(playerId))
            throw new KeyNotFoundException("Player not found.");
    }
    public IPlayer FindPlayer(Guid playerId)
    {
        if (_players.TryGetValue(playerId, out var player))
            return player;
        throw new KeyNotFoundException("Player not found.");
    }
    public List<IPlayer> GroupPlayersByScore()
    {
        return _players.Values.OrderByDescending(p => p.Score).ToList();
    }
}