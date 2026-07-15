using WorldRank.Domain.Enums;

namespace WorldRank.Domain.Exceptions;

public class WalletNotFoundException : WalletException
{
	public Guid PlayerId { get; }
	public Currency Currency { get; }

	public WalletNotFoundException(Guid playerId, Currency currency)
			: base($"Player {playerId} does not have a wallet in {currency}.")
	{
		PlayerId = playerId;
		Currency = currency;
	}
}
