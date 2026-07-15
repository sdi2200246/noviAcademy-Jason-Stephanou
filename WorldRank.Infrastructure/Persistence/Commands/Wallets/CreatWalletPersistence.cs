using WorldRank.Application.Commands;
using WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Persistence
{
    public class CreateWalletPersistence : ICreateWlletPersistence
    {
        private readonly WorldRankDbContext _appDbContext;

        public CreateWalletPersistence(WorldRankDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Persist(Wallet wallet , CancellationToken ct)
        {
            await _appDbContext.AddAsync(wallet , ct);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
