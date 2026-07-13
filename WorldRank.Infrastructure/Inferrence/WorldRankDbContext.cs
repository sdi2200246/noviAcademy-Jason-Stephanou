using WorldRank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class WorldRankDbContext:DbContext
{
    public WorldRankDbContext(DbContextOptions<WorldRankDbContext> options)
        : base(options) { }

    public DbSet<Player> Players => Set<Player>();
    public DbSet<Wallet> Wallets => Set<Wallet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorldRankDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
