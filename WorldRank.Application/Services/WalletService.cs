using WorldRank.Application.Interfaces;
using WorldRank.Application.Strategies;
using WorldRank.Domain.Entities;
using WorldRank.Domain.Enums;
using WorldRank.Domain.Exceptions;

namespace WorldRank.Application.Services;

public class WalletService : IWalletService
{
	private readonly IWalletRepository _walletRepository;
	private readonly IPlayerRepository _playerRepository;
	private readonly IReadOnlyDictionary<FundsOperation, IFundsStrategy> _fundsStrategies;

	public WalletService(
		IWalletRepository walletRepository,
		IPlayerRepository playerRepository,
		IEnumerable<IFundsStrategy> strategies)
	{
		_walletRepository = walletRepository;
		_playerRepository = playerRepository;
		_fundsStrategies = strategies.ToDictionary(strategy => strategy.Operation);
	}

	public async Task AddWalletToPlayer(int playerId, Currency currency, decimal balance , CancellationToken ct)
	{
		if (await _playerRepository.FindPlayer(playerId , ct) is null)
			throw new PlayerNotFoundException(playerId);

		var wallet = new Wallet(playerId, currency, balance);
		_walletRepository.Add(wallet);
	}

	public List<Wallet> GetWalletsOfPlayer(int playerId)
		=> _walletRepository.GetAllWalletsByPlayerId(playerId);

	public void DepositToWallet(int playerId, Currency currency, decimal amount)
		=> _walletRepository.Deposit(playerId, currency, amount);

	public void WithdrawFromWallet(int playerId, Currency currency, decimal amount)
		=> _walletRepository.Withdraw(playerId, currency, amount);

	public void BlockWallet(int playerId, Currency currency)
		=> _walletRepository.Block(playerId, currency);

	public void UnblockWallet(int playerId, Currency currency)
		=> _walletRepository.Unblock(playerId, currency);

	public void UpdateWalletBalance(int playerId, Currency currency, decimal newBalance)
		=> _walletRepository.UpdateBalance(playerId, currency, newBalance);

	public void ApplyFundsStrategy(int playerId, Currency currency, FundsOperation operation, decimal amount)
	{
		var strategy = _fundsStrategies[operation];
		var wallet = _walletRepository.GetWallet(playerId, currency);
		strategy.Execute(wallet, amount);
	}
}