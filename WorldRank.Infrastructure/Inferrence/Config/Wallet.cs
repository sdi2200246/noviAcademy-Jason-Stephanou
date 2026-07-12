using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldRank.Domain.Entities;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("wallets");
        builder.HasKey(w => w. WalletId);
        builder.Property(w => w.WalletId).ValueGeneratedNever();
        builder.Property(w => w.Balance);
        builder.Property(w => w.Currency);
        builder.Property(w => w.IsBlocked);
    }
}