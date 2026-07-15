// WorldRank.Infrastructure/Persistence/Querys/Players/ReadPlayerPersistenceCachingDecorator.cs
using WorldRank.Application.Interfaces;
using WorldRank.Application.Querys;
using WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Persistence.Querys
{
    public class ListPlayerPersistenceCachingDecorator : IReadPlayerPersistence
    {
        private readonly IReadPlayerPersistence _inner;
        private readonly ICache _cache;

         private readonly string  KeyPlayerString = "palyers";
        private static readonly TimeSpan Ttl = TimeSpan.FromSeconds(60);

        public ListPlayerPersistenceCachingDecorator(IReadPlayerPersistence inner, ICache cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public async Task<List<Player>> GetPlayersAsync(CancellationToken ct)
        {
            if (_cache.TryGet<List<Player>>(KeyPlayerString, out var cached))
                return cached;

            var players = await _inner.GetPlayersAsync(ct);

            _cache.Set(KeyPlayerString, players, Ttl);
            return players;
        }
    }
}