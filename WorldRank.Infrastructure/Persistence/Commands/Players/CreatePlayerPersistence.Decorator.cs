using WorldRank.Application.Commands;
using WorldRank.Application.Interfaces;
using WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Persistence.Commands
{
    public class PlayerPersistanceCacheDecorator : ICreatePlayerPersistence
    {
        
        private readonly ICreatePlayerPersistence _inner;
        private readonly ICache _cache;
        private readonly string  KeyPlayerString = "palyers";


        public PlayerPersistanceCacheDecorator(ICreatePlayerPersistence inner , ICache cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public async Task Persist(Player player , CancellationToken ct)
        {
            _cache.Remove(KeyPlayerString);
            await _inner.Persist(player , ct);
        }
    }
}
