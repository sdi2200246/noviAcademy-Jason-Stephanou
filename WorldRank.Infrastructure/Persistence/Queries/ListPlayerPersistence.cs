using Microsoft.EntityFrameworkCore;
using WorldRank.Application.Querys;
using WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Persistence.Querys
{
    public class ReadPlayerPersistence : IReadPlayerPersistence
    {
        private readonly WorldRankDbContext _appDbContext;

        public ReadPlayerPersistence(WorldRankDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Player>> GetPlayersAsync(CancellationToken ct)
        {
            return await _appDbContext.Players
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}