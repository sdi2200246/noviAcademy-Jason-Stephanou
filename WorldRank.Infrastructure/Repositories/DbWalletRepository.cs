using WorldRank.Domain.Entities;
using WorldRank.Application.Interfaces;
using WorldRank.Domain.Enums;
using WorldRank.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace WorldRank.Infrastructure.Repositories;

public class DbWalletRepository : IWalletRepository
{
    private readonly WorldRankDbContext _db;
    private readonly ILogger<DbWalletRepository> _logger;

    public DbWalletRepository(WorldRankDbContext db, ILogger<DbWalletRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public void Add(Wallet wallet)
    {
        _db.Wallets.Add(wallet);
        _db.SaveChanges();
    }

    public List<Wallet> GetAllWalletsByPlayerId(int playerId)
    {
        return _db.Wallets.AsNoTracking().Where(w => w.PlayerId == playerId).ToList();
    }

    public Wallet GetWallet(int playerId, Currency currency)
    {
       
        var wallet = _db.Wallets.FirstOrDefault(w => w.PlayerId == playerId && w.Currency == currency);

        if (wallet is null)
        {
            _logger.LogWarning(
                "Wallet NOT found for playerId={PlayerId}, currency={Currency} (int={CurrencyInt})",
                playerId, currency, (int)currency);
            throw new WalletNotFoundException(playerId, currency);
        }
        return wallet;
    }

    public void UpdateBalance(int playerId, Currency currency, decimal newBalance)
    {
        var wallet = GetWallet(playerId, currency);
        wallet.SetBalance(newBalance);
        _db.SaveChanges();
    }

    public void Withdraw(int playerId, Currency currency, decimal amount)
    {
        var wallet = GetWallet(playerId, currency);
        wallet.Withdraw(amount);
        _db.SaveChanges();
    }

    public void Block(int playerId, Currency currency)
    {
        var wallet = GetWallet(playerId, currency);
        wallet.Block();
        _db.SaveChanges();
    }

    public void Unblock(int playerId, Currency currency)
    {
        var wallet = GetWallet(playerId, currency);
        wallet.Unblock();
        _db.SaveChanges();
    }

    public void Deposit(int playerId, Currency currency, decimal amount)
    {
        var wallet = GetWallet(playerId, currency);
        wallet.Deposit(amount);
        _db.SaveChanges();
    }
}