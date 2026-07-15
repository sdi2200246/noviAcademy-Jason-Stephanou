using WorldRank.Application.Commands;
using WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Persistence.Commands
{
    public class CreatePlayerPersistence : ICreatePlayerPersistence
    {
        private readonly WorldRankDbContext _appDbContext;

        public CreatePlayerPersistence(WorldRankDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Persist(Player player , CancellationToken ct)
        {
            await _appDbContext.AddAsync(player , ct);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
