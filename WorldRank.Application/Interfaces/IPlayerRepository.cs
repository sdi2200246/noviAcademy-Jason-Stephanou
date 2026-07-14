using WorldRank.Domain.Entities;

namespace WorldRank.Application.Interfaces;

public interface IPlayerRepository
{
	Task AddPlayer(Player player , CancellationToken ct);

	Task<List<Player>> GetAllPlayers(CancellationToken ct);

	Task DeletePlayer(int playerId , CancellationToken ct);

	Task<Player?> FindPlayer(int playerId , CancellationToken ct);

	Task<List<Player>> GetByName(string name , CancellationToken ct);

	Task<IEnumerable<IGrouping<int, Player>>> GroupPlayersByScore(CancellationToken ct);
}
