using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class WorldRankDbContextFactory : IDesignTimeDbContextFactory<WorldRankDbContext>
{
    public WorldRankDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<WorldRankDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=worldrank;Username=postgres;Password=yourpassword")
            .Options;

        return new WorldRankDbContext(options);
    }
}