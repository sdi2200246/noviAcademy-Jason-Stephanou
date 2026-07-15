using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldRank.Domain.Entities;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("wallets");
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id).ValueGeneratedNever();
            builder.Property(w => w.Balance);
            builder.Property(w => w.Currency);
            builder.Property(w => w.IsBlocked);

        builder.HasOne<Player>()
            .WithMany(p => p.Wallets)
            .HasForeignKey(w => w.PlayerId)
            .IsRequired();

        builder.HasIndex(w => new { w.PlayerId, w.Currency })
            .IsUnique();
    }
}