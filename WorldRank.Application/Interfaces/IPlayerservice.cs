using WorldRank.Domain.Entities;
namespace WorldRank.Application.Interfaces;

public interface IPlayerService
{
	Task AddPlayer(string name, int score , CancellationToken ct);
	Task<List<Player>> ListPlayers(CancellationToken ct);
	Task<IReadOnlyDictionary<int, List<Player>>> ListPlayersByScore(CancellationToken ct);
	Task<List<Player>> FindPlayerByName(string name , CancellationToken ct);
	Task<Player?> FindPlayerById(int id , CancellationToken ct);
	Task DeletePlayer(int id , CancellationToken ct);
}
