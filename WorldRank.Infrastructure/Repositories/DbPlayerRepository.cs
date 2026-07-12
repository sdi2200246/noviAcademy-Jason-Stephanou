using WorldRank.Domain.Entities;
using WorldRank.Application.Interfaces;

namespace WorldRank.Infrastructure;


public class DbPlayerRepository : IPlayerRepository
{
    WorldRankDbContext _db {get;}
    public DbPlayerRepository(WorldRankDbContext db)
    {
        _db = db;
    }    
    
    public void AddPlayer(Player player)
    {
        _db.Players.Add(player);
        _db.SaveChanges();
    }

    public IEnumerable<Player> GetAllPlayers()
    {
        return _db.Players.ToList();
    }

    public void DeletePlayer(int playerId)
    {
        var player = _db.Players.Find(playerId);
        if (player is not null)
        {
            _db.Players.Remove(player);
            _db.SaveChanges();
        }
    }

    public Player? FindPlayer(int playerId)
    {
        return _db.Players.Find(playerId);
    }

    public IEnumerable<IGrouping<int, Player>> GroupPlayersByScore()
    {
        return _db.Players
            .GroupBy(p => p.Score)
            .ToList();
    }
}

