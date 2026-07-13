using WorldRank.Domain.Entities;
using WorldRank.Domain.Enums;
using WorldRank.Application.Strategies;

namespace WorldRank.Application.Interfaces;

public interface IWalletService
{
	Task AddWalletToPlayer(int playerId, Currency currency, decimal balance , CancellationToken ct);
	List<Wallet> GetWalletsOfPlayer(int playerId);
	void DepositToWallet(int playerId, Currency currency, decimal amount);
	void WithdrawFromWallet(int playerId, Currency currency, decimal amount);
	void BlockWallet(int playerId, Currency currency);
	void UnblockWallet(int playerId, Currency currency);
	void UpdateWalletBalance(int playerId, Currency currency, decimal newBalance);
	void ApplyFundsStrategy(int playerId, Currency currency, FundsOperation operation, decimal amount);
}