// WorldRank.Infrastructure/Persistence/Commands/Wallets/CreateWalletPersistenceCachingDecorator.cs
using WorldRank.Application.Commands;
using WorldRank.Application.Interfaces;
using WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Persistence.Commands
{
    public class CreateWalletPersistenceCachingDecorator : ICreateWlletPersistence
    {
        private readonly ICreateWlletPersistence _inner;
        private readonly ICache _cache;

        private const string AllWalletsKey = "wallets:all";

        public CreateWalletPersistenceCachingDecorator(ICreateWlletPersistence inner, ICache cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public async Task Persist(Wallet wallet, CancellationToken ct)
        {
            await _inner.Persist(wallet, ct);

            _cache.Remove(AllWalletsKey);
        }
    }
}