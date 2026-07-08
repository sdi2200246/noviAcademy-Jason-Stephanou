namespace WorldRank;

public interface IPlayer
{
    public IReadOnlyDictionary<Currency, IWallet> Wallets { get;}
    Guid Id { get; }
    string Name { get; }
    int Score { get; }
    void UpdateScore(int newScore);
    public void AddWallet(IWallet wallet);
}

public class Player : IPlayer
{
    private readonly Dictionary<Currency, IWallet> _wallets = new();
    public IReadOnlyDictionary<Currency, IWallet> Wallets => _wallets;
    public Guid Id { get; }
    public string Name { get; private set; }
    public int Score { get; private set; }

    public Player(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(nameof(name) , "Name cannot be null or empty.");

        Id = Guid.NewGuid();
        Name = name;
    }

    public void UpdateScore(int newScore)
    {
        if (newScore < 0)
            throw new ArgumentOutOfRangeException(nameof(newScore), "Score cannot be negative.");

        Score = newScore;
    }

    public override string ToString() => $"[{Id}] {Name} - Score: {Score}";

    public void AddWallet(IWallet wallet)
    {
        if (wallet is null)
            throw new ArgumentNullException(nameof(wallet), "Wallet cannot be null");

        if (!_wallets.TryAdd(wallet.Currency, wallet))
            throw new InvalidOperationException($"Wallet with currency:{wallet.Currency} already exists for user:{Name}");
    }
}

