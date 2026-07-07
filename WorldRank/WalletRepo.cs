using WorldRank;

public interface IWalletRepo
{
    public void AddWallet(IWallet wallet, Guid playerId);
    public List<IWallet> GetByPlayer(Guid playerId);
}

public class InMemWalletRepo : IWalletRepo
{
    Dictionary<Guid, IPlayer> Registry;
    public InMemWalletRepo(Dictionary<Guid, IPlayer> registry)
    {
        if (registry is null)
        {
            throw new ArgumentNullException("Repository cannot be null");
        }
        Registry = registry;
    }

    public void AddWallet(IWallet wallet, Guid playerId)
    {
        if (Registry.TryGetValue(playerId, out IPlayer? player))
        {
            player.AddWallet(wallet);
            return;
        }
        throw new KeyNotFoundException("Player not found.");
    }
    public List<IWallet> GetByPlayer(Guid playerId)
    {
        if (Registry.TryGetValue(playerId, out IPlayer? player))
            return player.Wallets.Values.ToList();

        return new List<IWallet>();
    }
}