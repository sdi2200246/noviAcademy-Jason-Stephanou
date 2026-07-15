namespace WorldRank.Domain.Exceptions;

public class PlayerNotFoundException : WorldRankException
{
	public Guid PlayerId { get; }

	public PlayerNotFoundException(Guid playerId)
			: base($"Player {playerId} was not found.")
	{
		PlayerId = playerId;
	}
}
