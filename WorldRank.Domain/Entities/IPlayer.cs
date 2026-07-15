namespace WorldRank.Domain.Entities;

public interface IPlayer
{
	Guid Id { get; }
	string Name { get; }
	int Score { get; }

	void AddScore(int points);
}
