using System.Net.NetworkInformation;
using WorldRank.Application.Interfaces;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Services;

public class PlayerService : IPlayerService
{
	private readonly IPlayerRepository _playerRepository;
	private readonly ICache _cacheMem;
	private const string PlayersCacheKey = "all_players";

	public PlayerService(IPlayerRepository playerRepository , ICache cacheMem)
	{
		_playerRepository = playerRepository;
		_cacheMem = cacheMem;
	}

	public async Task AddPlayer(string name, int score, CancellationToken ct)
	{
		var player = new Player(await GeneratePlayerId(ct), name);
		player.AddScore(score);
		await _playerRepository.AddPlayer(player, ct);

		_cacheMem.Remove(PlayersCacheKey); // list changed, invalidate
	}

	public async Task<List<Player>> ListPlayers(CancellationToken ct)
	{
		if (_cacheMem.TryGet(PlayersCacheKey, out List<Player>? cached))
			return cached!;

		var players = await _playerRepository.GetAllPlayers(ct);
		_cacheMem.Set(PlayersCacheKey, players, TimeSpan.FromSeconds(60));

		return players;
	}

	public async Task<IReadOnlyDictionary<int, List<Player>>> ListPlayersByScore(CancellationToken ct)
	{
		var groups = await _playerRepository.GroupPlayersByScore(ct);
		return groups.ToDictionary(g => g.Key, g => g.ToList());
	}

	public async Task<List<Player>> FindPlayerByName(string name, CancellationToken ct)
		=> await _playerRepository.GetByName(name, ct);

	public async Task<Player?> FindPlayerById(int id, CancellationToken ct)
		=> await _playerRepository.FindPlayer(id, ct);

	public async Task DeletePlayer(int id, CancellationToken ct)
		=> await _playerRepository.DeletePlayer(id, ct);

	private async Task<int> GeneratePlayerId(CancellationToken ct)
	{
		var existingIds = (await _playerRepository.GetAllPlayers(ct)).Select(p => p.Id).ToHashSet();
		int id;
		do { id = Random.Shared.Next(1, int.MaxValue); }
		while (existingIds.Contains(id));
		return id;
	}
}