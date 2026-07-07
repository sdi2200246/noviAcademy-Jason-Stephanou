using System.Runtime.InteropServices.Swift;

namespace WorldRank;

public interface IPlayer
{
    public Dictionary<Currency, IWallet> Wallets { get; }
    Guid Id { get; }
    string Name { get; }
    int Score { get; }
    void UpdateScore(int newScore);
    public void AddWallet(IWallet wallet);
}

public class Player : IPlayer
{
    public Dictionary<Currency, IWallet> Wallets { get; private set; } = new Dictionary<Currency, IWallet>();
    public Guid Id { get; }
    public string Name { get; private set; }
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

    public override string ToString() => $"[{Id}] {Name} - Score: {Score}";

    public void AddWallet(IWallet wallet)
    {
        if (wallet is null)
            throw new ArgumentNullException("Wallet cannot be null");

        if (!Wallets.TryAdd(wallet.Currency, wallet))
            throw new ArgumentException($"Wallet with currency:{wallet.Currency} already exists for user:{Name}");
    }
}

