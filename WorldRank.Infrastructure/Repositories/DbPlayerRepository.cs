using WorldRank.Domain.Entities;
using WorldRank.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WorldRank.Infrastructure.Repositories;


public class DbPlayerRepository : IPlayerRepository
{
	WorldRankDbContext _db { get; }
	public DbPlayerRepository(WorldRankDbContext db)
	{
		_db = db;
	}

	public async Task AddPlayer(Player player, CancellationToken ct)
	{
		await _db.Players.AddAsync(player, ct);
		await _db.SaveChangesAsync(ct);
	}

	public async Task<List<Player>> GetAllPlayers(CancellationToken ct)
	{
		return await _db.Players.AsNoTracking().ToListAsync(ct);
	}

	public async Task DeletePlayer(int playerId, CancellationToken ct)
	{
		var player = await _db.Players.FindAsync([playerId], ct);
		if (player is not null)
		{
			_db.Players.Remove(player);
			await _db.SaveChangesAsync(ct);
		}
	}

	public async Task<Player?> FindPlayer(int playerId, CancellationToken ct)
	{
		return await _db.Players.AsNoTracking().FirstOrDefaultAsync(p => p.Id == playerId, ct);
	}

	public async Task<List<Player>> GetByName(string name, CancellationToken ct)
	{
		return await _db.Players.AsNoTracking().Where(p => p.Name == name).ToListAsync(ct);
	}

	public async Task<IEnumerable<IGrouping<int, Player>>> GroupPlayersByScore(CancellationToken ct)
	{
		return await _db.Players
			.GroupBy(p => p.Score)
			.ToListAsync(ct);
	}
}